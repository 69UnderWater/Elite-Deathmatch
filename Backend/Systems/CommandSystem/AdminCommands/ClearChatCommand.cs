using System;
using Gangwar.DbModels.Models;
using Gangwar.Objects;
using GTANetworkAPI;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    public class ClearChatCommand : Script
    {
        [Command("clearchat")]
        public static void OnCommandClearChat(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return; 
                
                Accounts accounts = player.getData();
                if (accounts == null || accounts.AdminLevel < Convert.ToInt32(AdminRanksObject.STAFF)) return;

                int val = 0;

                do
                {
                    NAPI.Chat.SendChatMessageToAll($" ");
                    val++;
                } while (val < 100);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnCommandClearChat: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnCommandClearChat: {exception.Message}");
            }
        }
    }
}