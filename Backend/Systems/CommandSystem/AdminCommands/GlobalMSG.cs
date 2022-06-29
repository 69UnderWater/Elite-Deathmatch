using System;
using System.Linq;
using Gangwar.DbModels.Models;
using Gangwar.Objects;
using Gangwar.ServerModels;
using GTANetworkAPI;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    class GlobalMSG : Script
    {
        [Command("globalmsg", Alias = "gmsg", GreedyArg = true)]
        public void onGlobalMSG(Player player, string msg)
        {
            try
            {
                if (player == null || !player.Exists) return;
                Accounts accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;
                if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.STAFF)) return;

                NAPI.Chat.SendChatMessageToAll($"~w~[~r~ANNOUNCEMENT~w~] ~w~{player.Name}: {msg}");
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"GlobalMSGCMD: {exception.StackTrace}");
                Core.SendConsoleMessage($"GlobalMSGCMD: {exception.Message}");
            }
        }
    }
}