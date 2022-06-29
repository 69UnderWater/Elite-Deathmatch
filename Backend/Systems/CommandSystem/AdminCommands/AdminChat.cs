using Gangwar.Objects;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using Gangwar.DbModels.Models;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    public class AdminChat : Script
    {
        [Command("a", GreedyArg = true)]
        public void adminchat(Player player, string message)
        {
            try
            {
                if (player == null || !player.Exists) return;

                Accounts accData = player.getData();
                if (accData == null) return;
                if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.STAFF)) return;

                Core.sendAdminMessage("ADMIN", $"{player.Name}: {message}");
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage($"adminchat: {ex.StackTrace}");
                Core.SendConsoleMessage($"adminchat: {ex.Message}");
            }
        }
    }
}
