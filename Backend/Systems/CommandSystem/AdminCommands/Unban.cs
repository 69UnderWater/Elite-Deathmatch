using System;
using System.Linq;
using Gangwar.DbModels.Models;
using Gangwar.Objects;
using Gangwar.ServerModels;
using GTANetworkAPI;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    public class Unban : Script
    {
        [Command("unban", GreedyArg = true)]
        public void Bann(Player player, string playername)
        {
            try
            {
                if (player == null || !player.Exists) return;
                Accounts accData = player.getData();
                if (accData == null) return;
                if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.DEVELOPER)) return;
                Accounts targetData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.Username.ToLower() == playername.ToLower());
                if (targetData == null)
                {
                    player.sendLanguageToPlayer("[~r~BAN~w~] Kein Spieler gefunden.", "[~r~BAN~w~] No player found.", "[~r~Запрет~w~] Игрок не найден.");
                    return;
                }
                if (targetData.IsBanned == false)
                {
                    player.sendLanguageToPlayer("[~r~BAN~w~] Dieser Spieler ist nicht gebannt.", "[~r~BAN~w~] This Player isn't banned.", "[~r~Запрет~w~] Этот игрок не забанен.");
                    return;
                }

                player.sendLanguageToPlayer("[~r~BAN~w~] Du hast denn Spieler " + targetData.Username + " entbannt", "[~r~BAN~w~] You have unbanned the Player " + targetData.Username, "[~r~Запрет~w~] Вы разблокировали игрока " + targetData.Username);
                Core.sendwebhook("https://canary.discord.com/api/webhooks/900033946656571453/gT2hcKozTHTN2YAKDDDntZKz7It4dWJpBSy0mArhBTJs7qjeRONQvrhXXySc67UbW8Yb", "Globales Ban-System", targetData.Username + " wurde von " + accData.Username + " entbannt");
                
                targetData.IsBanned = false;
                targetData.BanReason = "";
                targetData.BanDate = DateTime.Now;
                targetData.IsTimeBanned = false;
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage($"Unbann: {ex.StackTrace}");
                Core.SendConsoleMessage($"Unbann: {ex.Message}");
            }
        }
    }
}