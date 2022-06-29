using System;
using System.Collections.Generic;
using System.Linq;
using GTANetworkAPI;
using Gangwar.ServerModels;
using Gangwar.DbModels;

namespace Gangwar.Systems.LanuageSystem
{
    public class Language : Script
    {
        [RemoteEvent("server:selectLanguage")]
        public static void OnServerSelectLanguage(Player player, int language)
        {
            try
            {
                if (player == null || !player.Exists) return;

                var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;

                accData.SelectedLanguage = language;

                using (gtaContext db = new gtaContext())
                {
                    db.Accounts.Update(accData);
                    db.SaveChanges();
                }

                player.SetData<int>("language", accData.SelectedLanguage);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnServerLoginRequestLogin: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnServerLoginRequestLogin: {exception.Message}");
            }
        }
    }
}
