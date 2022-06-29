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
    public class TeleportCMDs : Script
    {
        [Command("go")]
        public void CMD_gotoplayer(Player player, Player target)
        {
            try
            {
                if (player == null || !player.Exists || target == null || !target.Exists) return;
                Accounts accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;
                if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.STAFF)) return;

                player.Position = target.Position;
                player.Dimension = target.Dimension;
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"{exception.StackTrace}");
                Core.SendConsoleMessage($"{exception.Message}");
            }
        }

        [Command("get")]
        public static void CMD_getplayertome(Player player, Player target)
        {
            try
            {
                if (player == null || !player.Exists || target == null || !target.Exists) return;

                var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;

                if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.STAFF)) return;

                target.Position = player.Position;
                target.Dimension = player.Dimension;
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"{exception.StackTrace}");
                Core.SendConsoleMessage($"{exception.Message}");
            }
        }
    }
}
