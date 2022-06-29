using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Gangwar.Objects;
using Gangwar.ServerModels;

namespace Gangwar.Systems.InventorySystem.Items
{
    public class Geldsack : ItemObject
    {
        public Geldsack()
        {
            Id = 2;
            Name = "Geldsack";
        }

        public override bool getItemFunction(Player player)
        {
            int randomMoney = new Random().Next(10, 50);

            ServerAccountModel.SetPlayerMoney(player, randomMoney);
            player.sendLanguageToPlayer($"[~r~SERVER~w~] Du hast ein Geldsack geöffnet!", $"[~r~SERVER~w~] You have opened a money bag!", $"[~r~SERVER~w~] Вы открыли мешок с деньгами");
            return true;
        }
    }
}
