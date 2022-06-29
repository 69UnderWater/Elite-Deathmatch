using System;
using System.Collections.Generic;
using GTANetworkAPI;
using Gangwar.Objects;
using Gangwar.ServerModels;
using System.Linq;
using Gangwar.DbModels.Models;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    public class TimebanCommand : Script
    {
        [Command("timeban", GreedyArg = true)]
        public static void TimebanCMD(Player player, Player target, double timeBan, string reason)
        {
            try
            {
                if (player == null || !player.Exists || target == null || !target.Exists) return;
                Accounts accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;
                if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.STAFF)) return;
                
                Core.sendLanguageToEveryone("[~r~SERVER~w~] Der Spieler " + target.Name + " wurde für " + timeBan + " Stunden gebannt! Grund: " + reason, 
                    "[~r~SERVER~w~] The player " + target.Name + " got banned for " + timeBan + " hours! Reason: " + reason,
                    "[~r~СЕРВЕР~w~] Игрок " + target.Name + " был забанен на " + timeBan + " часа! Причина: " + reason);

                ServerAccountModel.SetPlayerTimeBan(target, timeBan);
                ServerAccountModel.SetPlayerTimeBanned(target, true);
                
                new Timer(() =>
                {
                    if (target == null || !target.Exists) return;

                    target.sendLanguageToPlayer($"[~r~SERVER~w~] Du wurdest bis zum {accData.TimeBanUntil} temporär gebannt! Grund: " + reason,
                        $"[~r~SERVER~w~] You were temporarily banned until {accData.TimeBanUntil}! Reason: " + reason,
                        $"[~r~СЕРВЕР~w~] Вы были временно заблокированы до {accData.TimeBanUntil}! Причина: " + reason);
                    target.Kick();
                }, 2500, "[TimebanCommand] Kick Player Timer");
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"TimebanCMD: {exception.StackTrace}");
                Core.SendConsoleMessage($"TimebanCMD: {exception.Message}");
            }
        }
    }
}
