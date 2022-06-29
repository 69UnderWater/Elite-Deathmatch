using System;
using System.Linq;
using Gangwar.DbModels.Models;
using Gangwar.Objects;
using Gangwar.ServerModels;
using GTANetworkAPI;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    public class Ban : Script
    {
        [Command("ban", GreedyArg = true)]
        public void Bann(Player player, Player target, string reason)
        {
            try
            {
                if (player == null || !player.Exists || target == null || !target.Exists) return;

                Accounts accData = player.getData();
                if (accData == null) return;
                if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.STAFF)) return;
                
                var targetData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.Username == target.Name);
                if (targetData == null)
                {
                    player.sendLanguageToPlayer("[~r~BAN~w~] Kein Spieler gefunden.", "[~r~BAN~w~] No player found.", "[~r~Запрет~w~] Игрок не найден.");
                    return;
                }

                Core.sendLanguageToEveryone("[~r~SERVER~w~] Der Spieler " + target.Name + " wurde gebannt! Grund: " + reason, 
                    "[~r~SERVER~w~] The player " + target.Name + " got banned Reason: " + reason,
                    "[~r~СЕРВЕР~w~] Игрок " + target.Name + " был забанен! Причина: " + reason);
                
                targetData.IsTimeBanned = false;
                targetData.IsBanned = true;
                targetData.BanReason = reason;
                
                target.Dimension = 10000;
                target.sendLanguageToPlayer("[~r~BAN~w~] Du wurdest von " + player.Name + " gebannt! Grund: " + reason, "[~r~BAN~w~] You got banned by " + player.Name + " for reason: " + reason, "[~r~Запрет~w~] Вы были убиты " + player.Name + " запрещено! Причина: " + reason);

                new Timer(() =>
                {
                    if (target == null || !target.Exists) return;
                    target.Kick("ban");
                }, 3000, "[Ban] Kick Player Timer");
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage($"Bann: {ex.StackTrace}");
                Core.SendConsoleMessage($"Bann: {ex.Message}");
            }
        }
    }
}