using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gangwar.Systems.AnitCheatSystem
{
    class AntiCheat : Script
    {

        [RemoteEvent("server:anticheat:callHealKey")]
        public void callHealKey(Player player, int allowedHealth, int currentHealth)
        {
            try
            {
                if (player == null || !player.Exists) return;

                Core.sendAdminMessage($"ADMIN", $"{player.Name} AC-HealKey");
                
                //Core.sendAdminMessage("ANTICHEAT", $"Healkey - {player.Name} | +{currentHealth - allowedHealth} Leben");
                NAPI.Chat.SendChatMessageToAll($"[~r~ANTICHEAT~w~] {player.Name} wurde gekickt, Grund: AC-HealKey");
                player.Kick();
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"callHealKey: {exception.StackTrace}");
                Core.SendConsoleMessage($"callHealKey: {exception.Message}");
            }
        }

        [RemoteEvent("server:anticheat:callGodMode")]
        public void callGodMode(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;
                
                Core.sendAdminMessage($"ADMIN", $"{player.Name} AC-GodMode");

                //Core.sendAdminMessage("ANTICHEAT", $"GODMODE - {player.Name}");
                NAPI.Chat.SendChatMessageToAll($"[~r~ANTICHEAT~w~] {player.Name} wurde gekickt, Grund: AC-GodMode");
                player.Kick();
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"callGodMode: {exception.StackTrace}");
                Core.SendConsoleMessage($"callGodMode: {exception.Message}");
            }
        }

        [RemoteEvent("server:anticheat:callRapidFire")]
        public void callRapidFire(Player player, int bulletCount)
        {
            try
            {
                if (player == null || !player.Exists) return;
                
                Core.sendAdminMessage($"ADMIN", $"{player.Name} AC-RapidFire");

                //Core.sendAdminMessage("ANTICHEAT", $"RAPIDFIRE - {player.Name} | {bulletCount} p.s");

                NAPI.Chat.SendChatMessageToAll($"[~r~ANTICHEAT~w~] {player.Name} wurde gekickt, Grund: AC-RapidFire");
                player.Kick();
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"callRapidFire: {exception.StackTrace}");
                Core.SendConsoleMessage($"callRapidFire: {exception.Message}");
            }
        }
    }
}
