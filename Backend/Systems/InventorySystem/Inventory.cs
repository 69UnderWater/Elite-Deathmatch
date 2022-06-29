using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Gangwar.DbModels.Models;
using Gangwar.ServerModels;
using Gangwar.Objects;
using Gangwar.Objects.MainMenuObjects;
using Newtonsoft.Json;

namespace Gangwar.Systems.InventorySystem
{
    public class Inventory : Script 
    {
        [RemoteEvent("Server:LocalStorage:RecivePlayerWeapons")]
        public static void recivePlayerWeapons(Player player, int shortgun, int longgun)
        {
            try
            {
                if (player == null || !player.Exists) return;

                Accounts accounts = player.getData();
                if (accounts == null) return;

                if (player.HasSharedData("IS_IN_FFA") || player.HasSharedData("IS_IN_OVO"))
                {
                    if(player.GetSharedData<bool>("IS_IN_FFA") || player.GetSharedData<bool>("IS_IN_OVO"))
                    {
                        player.sendLanguageToPlayer("[~r~INFO~w~] Du erhälst deine Waffen wenn du die FFA-Arena oder das 1vs1 verlassen hast.",
                            "[~r~INFO~w~] You will get your weapons when you leave the FFA Arena or the 1vs1",
                            "[~r~ИНФО~w~] Вы получите оружие, когда покинете арену FFA или 1vs1.");
                        return;
                    }
                }

                NAPI.Player.RemoveAllPlayerWeapons(player);
                NAPI.Player.GivePlayerWeapon(player, (uint)shortgun, 9999);
                NAPI.Player.GivePlayerWeapon(player, (uint)longgun, 9999);

                accounts.Weapons.WeaponOne = shortgun;
                accounts.Weapons.WeaponTwo = longgun;

                player.TriggerEvent("client:inventory:updateInventoryWeapons", accounts.Weapons.WeaponOne,
                    accounts.Weapons.WeaponTwo);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage("recivePlayerWeapons: " + exception.Message);
                Core.SendConsoleMessage("recivePlayerWeapons: " + exception.StackTrace);
            }
        }

        [RemoteEvent("Server:Inventory:RequestItems")]
        public static void OnServerInventoryRequestInventory(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;
                
                Accounts accounts = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accounts == null) return;

                var inventoryData = ServerInventoryModel.Inventory_.FirstOrDefault(x => x.SocialClubId == player.SocialClubId);
                if (inventoryData == null) return;

                // Show Inventory
                player.TriggerEvent("client:inventory:updateInventoryList", NAPI.Util.ToJson(inventoryData.Inventar));
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnServerInventoryRequestItems: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnServerInventoryRequestItems: {exception.Message}");
            }
        }

        [RemoteEvent("Server:Inventory:RequestWeapons")]
        public static void OnServerInventoryRequestWeapons(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;
                
                Accounts accounts = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accounts == null) return;

                DbModels.Models.Inventory inventoryData = ServerInventoryModel.Inventory_.FirstOrDefault(x => x.SocialClubId == player.SocialClubId);
                if (inventoryData == null) return;

                // Show Inventory
                player.TriggerEvent("client:inventory:updateInventoryWeapons", accounts.Weapons.WeaponOne,
                    accounts.Weapons.WeaponTwo);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnServerInventoryRequestItems: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnServerInventoryRequestItems: {exception.Message}");
            }
        }

        [RemoteEvent("Server:Inventory:UseItem")]
        public static void OnServerInventoryUseItem(Player player, string ItemName)
        {
            try
            {
                if (player == null || !player.Exists) return;

                if (ServerInventoryModel.HasItem(player.SocialClubId, ItemName))
                    ServerInventoryModel.UseItem(player, ItemName);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnServerInventoryRequestItems: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnServerInventoryRequestItems: {exception.Message}");
            }
        }

        [RemoteEvent("Server:Inventory:RemoveItem")]
        public static void OnServerInventoryRemoveItem(Player player, string ItemName)
        {
            try
            {
                if (player == null || !player.Exists) return;

                if (ServerInventoryModel.HasItem(player.SocialClubId, ItemName))
                    ServerInventoryModel.RemoveItem(player.SocialClubId, ItemName);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnServerInventoryRequestItems: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnServerInventoryRequestItems: {exception.Message}");
            }
        }
    }
}
