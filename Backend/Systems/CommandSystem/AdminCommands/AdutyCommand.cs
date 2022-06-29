using System;
using Gangwar.DbModels.Models;
using Gangwar.Objects;
using Gangwar.ServerModels;
using GTANetworkAPI;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    public class AdutyCommand : Script
    {
        [Command("aduty")]
        public static void AdutyCMD(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;

                Accounts accData = player.getData();
                if (accData == null) return;
                if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.STAFF)) return;

                if (accData.IsAduty)
                {
                    accData.IsAduty = false;
                    player.TriggerEvent("client:enableNametags", false);
                    player.TriggerEvent("Client:Aduty:Update", false);
                    player.SetSharedData("AdminTools:IsInvisible", false);
                    player.SetSharedData("AdminTools:SetInvincible", false);
                    player.sendLanguageToPlayer($"[~r~SERVER~w~] Du bist jetzt nicht mehr im Admin-Modus!", $"[~r~SERVER~w~] You deactivated Admin-Mode!", $"[~r~СЕРВЕР~w~] Вы деактивировали режим администратора!");
                }
                else
                {
                    accData.IsAduty = true;
                    player.TriggerEvent("client:enableNametags", true);
                    player.TriggerEvent("Client:Aduty:Update", true);
                    player.SetSharedData("AdminTools:IsInvisible", true);
                    player.SetSharedData("AdminTools:SetInvincible", true);
                    player.sendLanguageToPlayer($"[~r~SERVER~w~] Du bist jetzt im Admin-Modus!", $"[~r~SERVER~w~] You activated Admin-Mode!", $"[~r~СЕРВЕР~w~] Вы активировали режим администратора!");
                }
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage($"AdutyCMD: {ex.StackTrace}");
                Core.SendConsoleMessage($"AdutyCMD: {ex.Message}");
            }
        }
    }
}
