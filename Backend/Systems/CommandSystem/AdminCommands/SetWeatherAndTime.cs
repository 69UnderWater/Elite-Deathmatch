using Gangwar.Objects;
using Gangwar.ServerModels;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gangwar.DbModels.Models;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    class SetWeatherAndTime : Script
    {
        [Command("setweather")]
        public static void setWeather(Player player, int weather)
        {
            try
            {
                if (player == null || !player.Exists) return;
                Accounts accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;
                if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.DEVELOPER)) return;

                NAPI.World.SetWeather(Weather.CLEAR);

/*                Core.ServerConfig.WorldWeather = weather;
                player.SendChatMessage($"[~r~ADMIN~w~] Weather changed to " + weather);*/
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"AddFFACMD: {exception.StackTrace}");
                Core.SendConsoleMessage($"AddFFACMD: {exception.Message}");
            }
        }

        [Command("settime")]
        public static void setTime(Player player, int time, int time2, int time3)
        {
            try
            {
                if (player == null || !player.Exists) return;

                var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;

                if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.DEVELOPER)) return;

                NAPI.World.SetTime(time, time2, time3);

            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"AddFFACMD: {exception.StackTrace}");
                Core.SendConsoleMessage($"AddFFACMD: {exception.Message}");
            }
        }
    }
}
