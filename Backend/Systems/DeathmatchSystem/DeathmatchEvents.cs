using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GTANetworkAPI;
using Gangwar.DbModels.Models;
using Gangwar.DbModels;
using Gangwar.ServerModels;

namespace Gangwar.Systems.DeathmatchSystem
{
    public class DeathmatchEvents : Script
    {
        [RemoteEvent("Server:Deathmatch:WithdrawWinner")]
        public static void OnServerDeathmatchWithdrawWinner(Player player, int deathmatchId, string teamName)
        {
            try
            {
                if (player == null || !player.Exists) return;

                var deathmatchData = ServerDeathmatchModel.Deathmatch_.FirstOrDefault(x => x.DeathmatchId == deathmatchId);
                if (deathmatchData == null) return;

                if (deathmatchData.TeamOneName == teamName)
                {
                    foreach (var playerSocial in deathmatchData.Players)
                    {
                        var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == playerSocial);
                        if (accData == null) return;

                        Player players = NAPI.Player.GetPlayerFromName(accData.Username);
                        if (players == null || !players.Exists) return;

                        if (players.GetSharedData<string>("PLAYER_DEATHMATCH_TEAM_NAME") == teamName)
                        {
                            players.SendChatMessage($"[~r~Deathmatch~w~] Ihr habt das Deathmatch gewonnen!");
                            ServerInventoryModel.AddItem(players, "XPToken", 5);
                        }
                    }
                } 
                else if (deathmatchData.TeamTwoName == teamName)
                {
                    foreach (var playerSocial in deathmatchData.Players)
                    {
                        var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == playerSocial);
                        if (accData == null) return;

                        Player players = NAPI.Player.GetPlayerFromName(accData.Username);
                        if (players == null || !players.Exists) return;

                        if (players.GetSharedData<string>("PLAYER_DEATHMATCH_TEAM_NAME") == teamName)
                        {
                            players.SendChatMessage($"[~r~Deathmatch~w~] Ihr habt das Deathmatch gewonnen!");
                            ServerInventoryModel.AddItem(players, "XPToken", 5);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"{exception.StackTrace}");
                Core.SendConsoleMessage($"{exception.Message}");
            }
        }
    }
}
