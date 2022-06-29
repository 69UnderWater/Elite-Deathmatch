using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Gangwar.DbModels.Models;
using Gangwar.ServerModels;

namespace Gangwar.Systems.LoginSystem
{
    public class Login : Script
    {
        [RemoteEvent("Server:Login:RequestLogin")]
        public static async void OnServerLoginRequestLogin(Player player, string username)
        {
            try
            {
                if (player == null || !player.Exists) return;

                if (!ServerAccountModel.HasAccount(player.SocialClubId) && !ServerAccountModel.DoesUsernameExist(username))
                {
                    await ServerAccountModel.AddAccount(player, username);
                    //player.TriggerEvent("client:createLanguageBrowser");
                    player.TriggerEvent("client:mainmenu:serverregistercallback");
                    ServerInventoryModel.CreateInventory(player);
                }
                else
                {
                    if (ServerAccountModel.CheckLoginDetails(player.SocialClubId, username))
                    {
                        await ServerAccountModel.LoadAccount(player);
                    }
                    // TODO: Send message that account exists
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnServerLoginRequestLogin: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnServerLoginRequestLogin: {exception.Message}");
            }
        }
    }
}
