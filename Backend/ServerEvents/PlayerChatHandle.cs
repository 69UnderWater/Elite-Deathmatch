using System;
using System.Collections.Generic;
using System.Linq;
using Gangwar.DbModels;
using Gangwar.DbModels.Models;
using Gangwar.ServerModels;
using GTANetworkAPI;
using Microsoft.EntityFrameworkCore;

namespace Gangwar.ServerEvents
{
    public class PlayerChatHandle : Script
    {
        private static HashSet<string> WordBlacklist = new HashSet<string>()
        {
            "hurensohn",
            "nuttensohn",
            "toten",
            "nigger",
            "nigga",
            "transe",
            "schlampe",
            "schlampensohn",
            "fotze",
            "huan",
            "hrsn",
            "spast",
            "spasti",
            "hs",
            "ns",
            "nazi",
            "nuttenkind",
            "schwuchtel",
            "niggr"
        };

        [ServerEvent(Event.ChatMessage)]
        public static async void OnChatMessage(Player player, string message)
        {
            try
            {
                if (player == null || !player.Exists) return;

                if(message.Length > 256)
                {
                    player.sendLanguageToPlayer("[~r~SERVER~w~] Deine Nachricht ist zu lang.",
                    "[~r~SERVER~w~] Your message is to long.",
                    "[~r~СЕРВЕР~w~] Ваше сообщение слишком длинное.");
                    return;
                }

                var accData =
                    ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);

                string adminRank = ServerAdminRankModel.GetAdminRankName(accData.AdminLevel);
                
                bool containsResult = WordBlacklist.Any(x => message.ToLower().Contains(x));

                if (accData.Prestige < 1)
                {
                    NAPI.Chat.SendChatMessageToAll(
                        $"~w~[{ServerAdminRankModel.GetChatColor(adminRank) + adminRank}~w~] ~w~{player.Name} ~w~[~y~{accData.Level}~w~]: {message}");
                }
                else
                {
                    NAPI.Chat.SendChatMessageToAll(
                        $"~w~[{ServerAdminRankModel.GetChatColor(adminRank) + adminRank}~w~] ~w~{player.Name} ~w~[~y~Lvl: {accData.Level} ~w~| ~y~Prestige: {accData.Prestige}~w~]: {message}");
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnChatMessage: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnChatMessage: {exception.Message}");
            }
        }
    }
}
