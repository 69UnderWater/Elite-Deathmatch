using System;
using System.Collections.Generic;
using System.Text;
using Gangwar;
using Gangwar.ServerModels;
using Gangwar.Objects;
using Gangwar.DbModels;
using Gangwar.DbModels.Models;
using System.Linq;
using GTANetworkAPI;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    public class SetFrakCommand : Script
    {
        [Command("setpfrak")]
        public static void SetPrivateFrakCMD(Player player, Player target, int frakId, int frakRank = 0)
        {
            try
            {
                if (player == null || !player.Exists || target == null || !target.Exists) return;
                Accounts accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;
                Accounts tAccData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == target.SocialClubId);
                if (tAccData == null) return;
                Teams frakData = ServerTeamModel.Teams_.FirstOrDefault(x => x.TeamId == frakId);
                if (frakData == null) return;

                if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.DEVELOPER)) return;

                tAccData.IsPrivateFrak = true;
                tAccData.PrivateFrakId = frakData.TeamId;
                tAccData.PrivateFrakRank = frakRank;
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"SetPrivateFrakCMD: {exception.StackTrace}");
                Core.SendConsoleMessage($"SetPrivateFrakCMD: {exception.Message}");
            }
        }
    }
}
