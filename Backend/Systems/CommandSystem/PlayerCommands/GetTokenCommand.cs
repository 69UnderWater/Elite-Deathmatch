using System;
using System.Collections.Generic;
using System.Linq;
using GTANetworkAPI;
using Gangwar.ServerModels;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    public class GetTokenCommand : Script
    {
        [Command("gettoken")]
        public static void GetTokenCMD(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;

                var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;

                if (accData.discordSyncCode == "")
                {
                    string token = ServerAccountModel.RandomString();

                    if (!ServerAccountModel.DoesTokenExist(token))
                    {
                        ServerAccountModel.UpdateToken(player.SocialClubId, token);
                        Core.SendConsoleMessage($"{player.Name} | {token}");
                        player.SendChatMessage($"{token}");
                    }
                }
                else
                {
                    player.SendChatMessage($"{accData.discordSyncCode}");
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"GetTokenCMD: {exception.StackTrace}");
                Core.SendConsoleMessage($"GetTokenCMD: {exception.Message}");
            }
        }
    }
}
