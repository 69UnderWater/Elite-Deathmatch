using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gangwar.Systems.CommandSystem.PlayerCommands
{
    public class ReportSystem : Script
    {
        [Command("report", GreedyArg = true)]
        public static void reportotherplayer(Player player, Player target, string reason)
        {
            if (player == null || !player.Exists || target == null || !target.Exists || reason.Length == 0) return;

            player.sendLanguageToPlayer($"[~r~REPORT~w~] Du hast {target.Name} gemeldet, Grund: {reason}",
                $"[~r~REPORT~w~] You reported {target.Name}, reason: {reason}",
                $"[~r~ОТЧЕТ~w~] Вы сообщили {target.Name}, причина: {reason}");

            Core.sendAdminMessage("REPORT", $"{player.Name} hat {target.Name} reportet, Grund: {reason}");
        }
    }
}
