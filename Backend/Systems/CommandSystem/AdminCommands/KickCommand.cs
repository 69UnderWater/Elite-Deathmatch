using System;
using System.Collections.Generic;
using System.Linq;
using Gangwar.DbModels.Models;
using GTANetworkAPI;
using Gangwar.ServerModels;
using Gangwar.Objects;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    public class KickCommand : Script
    {
        [Command("kick", GreedyArg = true)]
        public static void KickCMD(Player player, Player target, string reason)
        {
            try
            {
                if (player == null || !player.Exists || target == null || !target.Exists) return;
                Accounts accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;

                if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.STAFF)) return;

                Core.sendLanguageToEveryone($"[~r~SERVER~w~] {target.Name} wurde von {player.Name} gekickt, Grund: {reason}",
                    $"[~r~SERVER~w~] {target.Name} got kicked by {player.Name}, reason: {reason}",
                    $"[~r~СЕРВЕР~w~] {target.Name} получил пинок от {player.Name}, причина: {reason}");

                player.SendChatMessage($"[~r~SERVER~w~] Du hast ~o~{target.Name} ~w~mit dem Grund: ~r~{reason} ~w~gekickt!");
                target.sendLanguageToPlayer($"[~r~SERVER~w~] Du wurdest gekickt. Grund: " + reason,
                    $"[~r~SERVER~w~] You've been kicked. Reason: " + reason,
                    $"[~r~СЕРВЕР~w~] Вы были удалены. Причина: " + reason);
                NAPI.Task.Run(() =>
                {
                    if(target == null || !target.Exists) return;
                    target.Kick();
                }, 3000);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"KickCMD: {exception.StackTrace}");
                Core.SendConsoleMessage($"KickCMD: {exception.Message}");
            }
        }
    }
}
