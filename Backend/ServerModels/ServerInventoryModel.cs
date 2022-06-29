using System;
using System.Collections.Generic;
using System.Linq;
using GTANetworkAPI;
using Gangwar.DbModels;
using Gangwar.DbModels.Models;
using Gangwar.Objects;
using Gangwar.Systems.InventorySystem.Items;

namespace Gangwar.ServerModels
{
    public class ServerInventoryModel : Script
    {
        public static List<Inventory> Inventory_ = new List<Inventory>();
        public static List<ItemObject> Items_ = new List<ItemObject>();

        public static void CreateInventory(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;

                Inventory inventoryData = new Inventory
                {
                    SocialClubId = player.SocialClubId,
                    Inventar = new List<InventoryObject>()
                };

                Inventory_.Add(inventoryData);

                using (gtaContext db = new gtaContext())
                {
                    db.Inventory.Add(inventoryData);
                    db.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"CreateInventory: {exception.StackTrace}");
                Core.SendConsoleMessage($"CreateInventory: {exception.Message}");
            }
        }

        public static void AddItem(Player player, string ItemName, int Amount = 1)
        {
            var inventoryData = Inventory_.FirstOrDefault(x => x.SocialClubId == player.SocialClubId);
            if (inventoryData == null) return;

            var itemData = inventoryData.Inventar.Find(x => x.Name == ItemName);
            if (itemData == null)
            {
                if (DoesItemExist(ItemName))
                {
                    inventoryData.Inventar.Add(new InventoryObject(ItemName, Amount));
                }
            }
            else
            {
                if (DoesItemExist(ItemName))
                    if (HasItem(player.SocialClubId, ItemName))
                        itemData.Amount += Amount;
            }

            player.TriggerEvent("client:inventory:updateInventoryList", NAPI.Util.ToJson(inventoryData.Inventar));
        }

        public static bool DoesItemExist(string ItemName)
        {
            var itemData = Items_.FirstOrDefault(x => x.Name == ItemName);
            if (itemData == null) return false;

            return true;
        }

        public static void RemoveItem(ulong socialClubId, string ItemName, int Amount = 1)
        {
            var inventoryData = Inventory_.FirstOrDefault(x => x.SocialClubId == socialClubId);
            if (inventoryData == null) return;

            var itemData = inventoryData.Inventar.Find(x => x.Name == ItemName);
            if (itemData == null) return;

            if (HasItem(socialClubId, ItemName))
                itemData.Amount -= Amount;

            using (gtaContext db = new gtaContext())
            {
                db.Inventory.Update(inventoryData);
                db.SaveChanges();
            }
        }

        public static bool HasItem(ulong socialClubId, string Name)
        {
            var inventoryData = Inventory_.FirstOrDefault(x => x.SocialClubId == socialClubId);
            if (inventoryData == null) return false;

            var itemData = inventoryData.Inventar.Find(x => x.Name == Name);
            if (itemData == null) return false;

            return true;
        }

        public static int ItemAmount(ulong socialClubId, string Name)
        {
            var inventoryData = Inventory_.FirstOrDefault(x => x.SocialClubId == socialClubId);
            if (inventoryData == null) return -1;

            var itemData = inventoryData.Inventar.Find(x => x.Name == Name);
            if (itemData == null) return -1;

            return itemData.Amount;
        }

        public static void UseItem(Player player, string Name)
        {
            try
            {
                if (player == null || !player.Exists) return;

                var inventoryData = Inventory_.FirstOrDefault(x => x.SocialClubId == player.SocialClubId);
                if (inventoryData == null) return;

                var playerItemData = inventoryData.Inventar.FirstOrDefault(x => x.Name == Name);
                if (playerItemData == null) return;

                ItemObject itemData = Items_.FirstOrDefault(x => x.Name == Name);
                if (itemData == null) return;

                int index = inventoryData.Inventar.IndexOf(playerItemData);

                if (HasItem(player.SocialClubId, Name))
                {
                    if (itemData.getItemFunction(player))
                    {
                        if (playerItemData.Amount == 1)
                            inventoryData.Inventar.Remove(playerItemData);
                        else
                        {
                            playerItemData.Amount = playerItemData.Amount - 1;
                            inventoryData.Inventar[index] = playerItemData;
                        }
                    }
                }

                using (gtaContext db = new gtaContext())
                {
                    db.Inventory.Update(inventoryData);
                    db.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"UseItem: {exception.StackTrace}");
                Core.SendConsoleMessage($"UseItem: {exception.Message}");
            }
        }

        public static void LoadItems()
        {
            // ITEMS TO LOAD:
            Items_.Add(new XPToken());
            Items_.Add(new Geldsack());
        }
    }
}
