using System;
using System.Collections.Generic;
using System.Linq;
using GTANetworkAPI;
using Gangwar.ServerModels;
using Gangwar.Objects;

namespace Gangwar.Systems.StatsSystem
{
    public class Stats : Script
    {
        [RemoteEvent("Server:MainMenu:RequestStats")]
        public static void OnServerMainMenuRequestStats(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;

                var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;

                player.TriggerEvent("Client:MainMenu:SetStats", accData.Username, accData.Kills, accData.Deaths, accData.Money, accData.Level, accData.PlayedHours);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnServerMainMenuRequestStats: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnServerMainMenuRequestStats: {exception.Message}");
            }
        }
    }
}
