using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using Gangwar.ServerModels;

namespace Gangwar.Systems.EventSystems
{
    public class WeaponTruck : Script
    {
        public static bool isTruckActive = false;
        public static Timer WeaponTruckStartTimer = null;
        public static Timer WeaponTruckTimer = null;
        public static Timer WeaponTruckBlipTimer = null;
        public static ColShape TruckApplyShape = null;
        public static Blip TruckEventBlip = null;
        public static Vehicle WeaponTruckVehicle = null;
        public static Blip TruckReturnBlip = null;

        public static void startWeaponTruckTimer()
        {
            if (isTruckActive == true) return;
            WeaponTruckStartTimer = new Timer(() =>
            {
                startWeaponTruckEvent();
            }, 1000 * 60 * 30, "[WeaponTruck] Weapon Truck Start Timer");
        }

        public static void startWeaponTruckEvent()
        {
            try
            {
                int Id = new Random().Next(1, ServerWeaponTruckModel.WeaponTruck_.Count);

                isTruckActive = true;
                //NAPI.Chat.SendChatMessageToAll($"[~r~EVENT~w~] Es wurde ein Waffentruck mit illegalen Gegenständen gesichtet.");
                Core.sendLanguageToEveryone($"[~r~EVENT~w~] Es wurde ein Waffentruck mit illegalen Gegenständen gesichtet.",
                    $"[~r~EVENT~w~] A weapontruck carrying illegal items has been spotted.",
                    $"[~r~СОБЫТИЕ~w~] Замечен грузовик, перевозивший запрещенные предметы.");
                WeaponTruckVehicle = NAPI.Vehicle.CreateVehicle(VehicleHash.Mule4, ServerWeaponTruckModel.GetRandomWeaponTruckSpawnPosition(Id).Position, ServerWeaponTruckModel.GetRandomWeaponTruckSpawnPosition(Id).Rotation, 160, 160, "EVENT", 255, false, true, 0);
                WeaponTruckVehicle.SetMod(16, 4);
                WeaponTruckVehicle.SetData<bool>("weapontruck", true);
                TruckEventBlip = NAPI.Blip.CreateBlip(67, WeaponTruckVehicle.Position, 1f, 1, "EVENT: Waffentruck", 255, 0, false, 0, 0);
                TruckReturnBlip = NAPI.Blip.CreateBlip(50, ServerWeaponTruckModel.GetRandomWeaponTruckReturnPosition(Id), 1f, 1, "EVENT: Abgabepunkt", 255, 0, true, 0, 0);
                TruckApplyShape = NAPI.ColShape.CreateSphereColShape(ServerWeaponTruckModel.GetRandomWeaponTruckReturnPosition(Id), 5f, 0);
                TruckApplyShape.SetData<bool>("WeaponTruckEventShape", true);

                WeaponTruckBlipTimer = new Timer(() =>
                {
                    if (TruckEventBlip == null || !TruckEventBlip.Exists) return;
                    TruckEventBlip.Position = WeaponTruckVehicle.Position;
                }, 1000 * 2, "[WeaponTruck] Blip Timer", 0);

                WeaponTruckTimer = new Timer(() =>
                {
                    //NAPI.Chat.SendChatMessageToAll("[~r~EVENT~w~] Leider konnte kein Team den Waffentruck abgeben, viel Glück beim nächsten mal!");
                    Core.sendLanguageToEveryone("[~r~EVENT~w~] Leider konnte kein Team den Waffentruck abgeben, viel Glück beim nächsten mal!",
                        "[~r~EVENT~w~] Unfortunately no team could deliver the weapon truck, good luck next time!",
                        "[~r~СОБЫТИЕ~w~] К сожалению, ни одна команда не смогла доставить грузовик с оружием, удачи в следующий раз!");
                    WeaponTruckBlipTimer.Kill();
                    TruckEventBlip.Dimension = 10000;
                    TruckEventBlip.Delete();
                    TruckApplyShape.Delete();
                    TruckReturnBlip.Dimension = 10000;
                    TruckReturnBlip.Delete();
                    WeaponTruckVehicle.Delete();

                    NAPI.Task.Run(() =>
                    {
                        isTruckActive = false;
                        WeaponTruckStartTimer = null;
                        WeaponTruckTimer = null;
                        WeaponTruckBlipTimer = null;
                        TruckApplyShape = null;
                        TruckEventBlip = null;
                        WeaponTruckVehicle = null;
                        TruckReturnBlip = null;
                        
                        startWeaponTruckTimer();
                    }, 100);
                }, 1000 * 60 * 20, "[WeaponTruck] Weapon Truck delete Timer");
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage("startWeaponTruckEvent: " + exception.Message);
                Core.SendConsoleMessage("startWeaponTruckEvent: " + exception.StackTrace);
            }
        }

        [Command("truckpos")]
        public void cmd_truckpos(Player player)
        {
            if (player == null || !player.Exists) return;
            Core.SendConsoleMessage("{\"Position\":" + NAPI.Util.ToJson(player.Position) + ",\"Rotation\":" + NAPI.Util.ToJson(player.Rotation) + "}");
            Core.SendConsoleMessage($"JSON: {NAPI.Util.ToJson(player.Rotation)}");
        }
    }
}
