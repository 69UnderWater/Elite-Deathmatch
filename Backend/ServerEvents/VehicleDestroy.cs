using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using Gangwar.Systems.EventSystems;
using System.Threading.Tasks;

namespace Gangwar.ServerEvents
{
    public class VehicleDestroy : Script
    {
        [ServerEvent(Event.VehicleDeath)]
        public void OnVehicleDeath(Vehicle vehicle)
        {
            try
            {
                if (vehicle == null || !vehicle.Exists) return;

                if (vehicle.HasData("weapontruck"))
                {
                    Core.sendLanguageToEveryone("[~r~EVENT~w~] Der Waffentruck wurde zerstört, viel Glück beim nächsten mal!",
                        "[~r~EVENT~w~] The weapontruck was destroyed, good luck next time!",
                        "[~r~СОБЫТИЕ~w~] Оружейный грузовик был уничтожен, удачи в следующий раз!");
                    WeaponTruck.WeaponTruckBlipTimer.Kill();
                    WeaponTruck.WeaponTruckTimer.Kill();
                    WeaponTruck.TruckEventBlip.Dimension = 10000;
                    WeaponTruck.TruckEventBlip.Delete();
                    WeaponTruck.TruckApplyShape.Delete();
                    WeaponTruck.TruckReturnBlip.Dimension = 10000;
                    WeaponTruck.TruckReturnBlip.Delete();
                    WeaponTruck.WeaponTruckVehicle.Delete();

                    NAPI.Task.Run(() =>
                    {
                        WeaponTruck.isTruckActive = false;
                        WeaponTruck.WeaponTruckStartTimer = null;
                        WeaponTruck.WeaponTruckTimer = null;
                        WeaponTruck.WeaponTruckBlipTimer = null;
                        WeaponTruck.TruckApplyShape = null;
                        WeaponTruck.TruckEventBlip = null;
                        WeaponTruck.WeaponTruckVehicle = null;
                        WeaponTruck.TruckReturnBlip = null;

                        WeaponTruck.startWeaponTruckTimer();
                    }, 100);
                    return;
                }

                vehicle.Delete();
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage("RM_DestoryVehicle: " + exception.Message);
                Core.SendConsoleMessage("RM_DestoryVehicle: " + exception.StackTrace);
            }
        }
    }
}
