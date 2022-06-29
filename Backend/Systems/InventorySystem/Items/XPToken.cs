using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Gangwar.Objects;
using Gangwar.ServerModels;

namespace Gangwar.Systems.InventorySystem.Items
{
    public class XPToken : ItemObject
    {
        public XPToken()
        {
            Id = 1;
            Name = "XPToken";
        }

        public override bool getItemFunction(Player player)
        {
            int randomXP = new Random().Next(0, 10);

            ServerAccountModel.SetPlayerEXP(player.SocialClubId, ServerAccountModel.GetPlayerEXP(player.SocialClubId) + randomXP);
            player.sendLanguageToPlayer($"[~r~SERVER~w~] Du hast ein XP-Token benutzt und {randomXP} XP erhalten.", $"[~r~SERVER~w~] You have used a XP-Token and got {randomXP} XP.", $"[~r~SERVER~w~] Вы использовали XP-токен");
            return true;
        }
    }
}
