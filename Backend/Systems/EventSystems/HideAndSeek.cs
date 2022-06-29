using System;
using System.Linq;
using GTANetworkAPI;
using Gangwar.ServerModels;

namespace Gangwar.Systems.EventSystems
{
    public class HideAndSeek : Script
    {
        public static bool isDealerActive = false;
        public static Timer DealerStartTimer = null;
        public static Timer DealerTimer = null;
        public static Timer DealerBlipTimer = null;
        public static Ped DealerPed = null;
        public static Ped DealerApplyPed = null;
        public static Blip DealerEventBlip = null;
        public static Blip DealerEventPlayerBlip = null;
        public static Blip DealerApplyBlip = null;
        public static GTANetworkAPI.Object DealerLootDrop = null;

        private static int PrivateDealerId = 0;

        public static void startDealerTimer()
        {
            try
            {
                if (isDealerActive == true) return;

                DealerStartTimer = new Timer(() =>
                {
                    startDealerEvent();
                }, 1000 * 60 * 20, "[HideAndSeek] Start Timer");
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage("startDealerTimer: " + exception.Message);
                Core.SendConsoleMessage("startDealerTimer: " + exception.StackTrace);
            }
        }

        public static void startDealerBlipTimer()
        {
            try
            {
                if (isDealerActive == false) return;

                DealerBlipTimer = new Timer(() =>
                {
                    Player player = NAPI.Pools.GetAllPlayers().Find(x => x.HasData("DealerLoot"));
                    if(player == null || !player.Exists) return;
                    DealerEventPlayerBlip.Position = player.Position;
                }, 1000 * 2, "[HideAndSeek] Blip Timer", 0);
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage($"startDealerBlipTimer: {ex.Message}");
                Core.SendConsoleMessage($"startDealerBlipTimer: {ex.StackTrace}");
            }
        }

        public static void startDealerEvent()
        {
            try
            {
                PrivateDealerId = new Random().Next(1, ServerDealerModel.Dealer_.Count);

                var dealerId = ServerDealerModel.Dealer_.FirstOrDefault(x => x.DealerId == PrivateDealerId);
                if (dealerId == null) return;

                isDealerActive = true;
                //NAPI.Chat.SendChatMessageToAll($"[~r~EVENT~w~] Es wurde ein Dealer in ~b~{dealerId.LocationName}~w~ gesichtet, finde ihn und schnapp dir die Beute!");
                Core.sendLanguageToEveryone($"[~r~EVENT~w~] Es wurde ein Dealer in ~b~{dealerId.LocationName}~w~ gesichtet, finde ihn und schnapp dir die Beute!",
                    $"[~r~EVENT~w~] A dealer has been spotted in ~b~{dealerId.LocationName}~w~, find him and grab the loot!",
                    $"[~r~СОБЫТИЕ~w~] Торговец был замечен в ~b~{dealerId.LocationName}~w~, найдите его и возьмите добычу!");
                DealerPed = NAPI.Ped.CreatePed((uint)PedHash.G, dealerId.DealerPosition, dealerId.DealerRotation.Z, false, true, true, true, 0);
                DealerEventBlip = NAPI.Blip.CreateBlip(456, DealerPed.Position, 1f, 1, "Dealer", 255, 0, true, 0, 0);
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage($"startDealerEvent: {ex.Message}");
                Core.SendConsoleMessage($"startDealerEvent: {ex.StackTrace}");
            }
        }

        public static void getDealerReward(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;
                if (player.Health <= 0) return;

                var dealerId = ServerDealerModel.Dealer_.FirstOrDefault(x => x.DealerId == PrivateDealerId);
                if (dealerId == null) return;

                if (isDealerActive == false)
                {
                    //player.SendChatMessage("[~r~DEALER~w~] Hier gibt es nichts mehr zu holen.");
                    player.sendLanguageToPlayer("[~r~DEALER~w~] Hier gibt es nichts mehr zu holen.",
                        "[~r~DEALER~w~] There is nothing left to get here.",
                        "[~r~ДИЛЕР~w~] Здесь уже ничего не осталось.");
                    return;
                }

                //NAPI.Chat.SendChatMessageToAll($"[~r~EVENT~w~] {player.Name} hat die Beute.");//ToDo: Better text
                Core.sendLanguageToEveryone($"[~r~EVENT~w~] {player.Name} hat die Beute.",
                    $"[~r~EVENT~w~] {player.Name} got the loot from the dealer.",
                    $"[~r~СОБЫТИЕ~w~] {player.Name} получил добычу от торговца.");

                player.SetClothes(5, 82, 0);
                player.SetData<bool>("DealerLoot", true);
                DealerEventPlayerBlip = NAPI.Blip.CreateBlip(676, player.Position, 1f, 30, "EVENT: Dealer-Loot", 255, 0, false, 0, 0);
                //ToDo: Give item or reward

                DealerEventBlip.Delete();
                DealerPed.Delete();

                DealerApplyPed = NAPI.Ped.CreatePed((uint)PedHash.Tramp01AMO, dealerId.AbgabeDealerPosition, dealerId.AbgabeDealerRotation.Z, false, true, true, true, 0);

                startDealerBlipTimer();

                NAPI.Task.Run(() =>
                {
                    DealerStartTimer = null;
                    DealerTimer = null;
                    DealerPed = null;
                    DealerEventBlip = null;

                    DealerApplyBlip = NAPI.Blip.CreateBlip(496, DealerApplyPed.Position, 1f, 2, "EVENT: Dealer", 255, 0, false, 0, 0);
                }, 100);
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage($"getDealerReward: {ex.Message}");
                Core.SendConsoleMessage($"getDealerReward: {ex.StackTrace}");
            }
        }

        public static void getDealerLootFromFloor(Player player)
        {
            try
            {
                if (player == null || !player.Exists || DealerLootDrop == null) return;
                if (player.Health <= 0) return;
                DealerLootDrop.Delete();
                player.SetClothes(5, 82, 0);
                player.SetData<bool>("DealerLoot", true);
                startDealerBlipTimer();

                NAPI.Task.Run(() =>
                {
                    DealerLootDrop = null;
                }, 100);
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage($"getDealerLootFromFloor: {ex.Message}");
                Core.SendConsoleMessage($"getDealerLootFromFloor: {ex.StackTrace}");
            }
        }

        public static void submitDealerLoot(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;
                if (player.Health <= 0) return;
                if (isDealerActive == false)
                {
                    //player.SendChatMessage("[~r~DEALER~w~] Hier gibt es nichts mehr, zieh ab!");
                    player.sendLanguageToPlayer("[~r~DEALER~w~] Hier gibt es nichts mehr, zieh ab!",
                        "[~r~DEALER~w~] Nothing left here, pull off!",
                        "[~r~ДИЛЕР~w~] Здесь ничего не осталось, снимай!");
                    return;
                }

                if (!player.HasData("DealerLoot"))
                {
                    player.sendLanguageToPlayer("[~r~DEALER~w~] Du hast nichts für mich, zieh ab!",
                        "[~r~DEALER~w~] You have nothing for me, pull off!",
                        "[~r~ДИЛЕР~w~] У тебя ничего нет для меня, отвали!");
                    return;
                }

                player.ResetData("DealerLoot");
                player.SetClothes(5, 0, 0);
                DealerBlipTimer.Kill();

                isDealerActive = false;

                DealerEventPlayerBlip.Delete();
                DealerApplyBlip.Delete();
                DealerApplyPed.Delete();

                NAPI.Task.Run(() =>
                {
                    DealerEventPlayerBlip = null;
                    DealerApplyBlip = null;
                    DealerApplyPed = null;
                    DealerBlipTimer = null;
                    PrivateDealerId = 0;

                    startDealerTimer();

                    if(player != null && player.Exists)
                    {
                        Core.sendLanguageToEveryone($"[~r~EVENT~w~] {player.Name} hat die Beute abgegeben und einen XP-Token erhalten.",
                        $"[~r~EVENT~w~] {player.Name} has delivered the loot and received XP-Token.",
                        $"[~r~СОБЫТИЕ~w~] {player.Name} доставил добычу и получил XP-Token.");
                        ServerInventoryModel.AddItem(player, "XPToken", 1);
                    }

                    if (DealerLootDrop != null)
                    {
                        DealerLootDrop = null;
                    }
                }, 100);
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage($"submitDealerLoot: {ex.Message}");
                Core.SendConsoleMessage($"submitDealerLoot: {ex.StackTrace}");
            }
        }
    }
}
