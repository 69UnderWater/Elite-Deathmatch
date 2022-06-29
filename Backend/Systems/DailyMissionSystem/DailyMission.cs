using System;
using System.Collections.Generic;
using System.Linq;
using Gangwar.DbModels.Models;
using GTANetworkAPI;
using Gangwar.ServerModels;

namespace Gangwar.Systems.DailyMissionSystem
{
    public class DailyMission : Script
    {
        [RemoteEvent("Server:MainMenu:RequestDailyMissions")]
        public static void OnServerMainMenuRequestDailyMissions(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;

                Accounts accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;

                accData.IsStreetFight = false;
                accData.IsFFA = false;
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnServerMainMenuRequestDailyMissions: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnServerMainMenuRequestDailyMissions: {exception.Message}");
            }
        }
    }
}
