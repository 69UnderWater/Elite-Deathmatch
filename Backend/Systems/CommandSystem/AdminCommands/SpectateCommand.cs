using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Gangwar.ServerModels;
using Gangwar.Objects;
using System.Linq;
using Gangwar.DbModels.Models;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    public class SpectateCommand : Script
    {
        [Command("spectate")]
        public static void SpectateCMD(Player player, Player target)
        {
            try
            {
                if (player == null || !player.Exists) return;
                Accounts accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;
                if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.STAFF)) return;
                
                if (target == null || !target.Exists)
                {
                    player.SendChatMessage("Der Spieler konnte nicht gefunden werden.");
                    return;
                }

                accData.IsSpectating = !accData.IsSpectating;
                player.Dimension = target.Dimension;
                player.TriggerEvent("client:admin:startspectate", target);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"SpectateCMD: {exception.StackTrace}");
                Core.SendConsoleMessage($"SpectateCMD: {exception.Message}");
            }
        }
    }
}
