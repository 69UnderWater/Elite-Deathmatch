using System;
using System.Linq;
using Gangwar.DbModels;
using Gangwar.DbModels.Models;
using Gangwar.Objects;
using Gangwar.ServerModels;
using GTANetworkAPI;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    public class Offlineban : Script
    {
        [Command("banid", GreedyArg = true)]
        public void BanId(Player player, int accId, int hours, string reason)
        {
            try
            {
                if (player == null || !player.Exists) return;
                Accounts accData = player.getData();
                if (accData == null) return;
                if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.DEVELOPER)) return;
                
                var targetData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.Id == accId);
                if (targetData == null)
                {
                    player.sendLanguageToPlayer("[~r~BAN~w~] Es existiert kein Spieler mit dieser Account-Id.", "[~r~BAN~w~] There is no player with this Account-Id.", "[~r~Запрет~w~] Нет игрока с таким идентификатором учетной записи.");
                    return;
                }

                if (hours == 0)
                {
                    targetData.IsBanned = true;
                    targetData.IsTimeBanned = false;
                    targetData.BanReason = reason;
                }
                else
                {
                    targetData.IsTimeBanned = true;
                    targetData.IsBanned = true;
                    targetData.TimeBanUntil = DateTime.Now.AddHours(hours);
                    targetData.BanReason = reason; 
                }

                player.sendLanguageToPlayer("[~r~BAN~w~] Du hast erfolgreich den Spieler: " + targetData.Username + " gebannt!", "[~r~BAN~w~] You successfully banned the player: " + targetData.Username, "[~r~Запрет~w~] Вы успешно запретили игрока: " + targetData.Username);
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage($"BanId: {ex.StackTrace}");
                Core.SendConsoleMessage($"BanId: {ex.Message}");
            }
        }
    }
}