using System;
using Gangwar.DbModels.Models;
using Gangwar.Objects;
using GTANetworkAPI;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    public class VanishCommand : Script
    {
        [Command("vanish")]
        public void OnPlayerVanishCommand(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;

                Accounts accounts = player.getData();
                if (accounts == null || accounts.AdminLevel < Convert.ToInt32(AdminRanksObject.STAFF)) return;

                accounts.IsVanish = !accounts.IsVanish;

                if (!accounts.IsVanish)
                {
                    player.SetSharedData("AdminTools:IsInvisible", true);
                    player.sendLanguageToPlayer("Du bist im vanish", "you are in vanish", "dings hier vanish");
                }
                else
                {
                    player.SetSharedData("AdminTools:IsInvisible", false);
                    player.sendLanguageToPlayer("Du bist nicht mehr im vanish", "you aren't in vanish", "dings hier kein vanish");
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnPlayerVanishCommand: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnPlayerVanishCommand: {exception.Message}");
            }
        }
    }
}