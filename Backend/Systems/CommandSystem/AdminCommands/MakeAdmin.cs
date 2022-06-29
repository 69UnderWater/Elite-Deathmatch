using System;
using System.Linq;
using Gangwar.DbModels.Models;
using Gangwar.Objects;
using Gangwar.ServerModels;
using GTANetworkAPI;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    public class MakeAdmin : Script
    {
        [Command("makeadmin")]
        public void makeAdmin(Player player, int accId, int adminLevel)
        {
            try
            {
                if (player == null || !player.Exists) return;

                Accounts accData = player.getData();
                if (accData == null) return;
                if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.DEVELOPER)) return;

                var targetData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.Id == accId);
                if (targetData == null) return;

                targetData.AdminLevel = adminLevel;

                player.SendChatMessage("[~r~ADMIN~w~] Du hast dem Spieler " + targetData.Username + " den AdminRang: " + adminLevel + " gegeben!");
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage($"makeAdmin: {ex.StackTrace}");
                Core.SendConsoleMessage($"makeAdmin: {ex.Message}");
            }
        }
    }
}