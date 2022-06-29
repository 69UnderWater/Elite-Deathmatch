using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using System.Linq;
using Gangwar.DbModels.Models;
using Gangwar.DbModels;
using Gangwar.ServerModels;

namespace Gangwar.Systems.PlayedHoursSystem
{
    public class PlayedHours : Script
    {
        public static void UpdatePlayedHours(ulong socialClubId, int minutesAdded)
        {
            try
            {
                var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == socialClubId);
                if (accData == null) return;

                accData.PlayedHours += minutesAdded;
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"UpdatePlayedHours: {exception.StackTrace}");
                Core.SendConsoleMessage($"UpdatePlayedHours: {exception.Message}");
            }
        }
    }
}
