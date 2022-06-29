using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace Gangwar.ServerEvents
{
    public class PlayerVehicleHandle : Script
    {
        [ServerEvent(Event.PlayerEnterVehicle)]
        public static void OnPlayerEnterVehicle(Player player, Vehicle vehicle, sbyte seat)
        {
            try
            {
                if (player == null || !player.Exists || vehicle == null || !vehicle.Exists) return;
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnPlayerEnterVehicle: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnPlayerEnterVehicle: {exception.Message}");
            }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public static void OnPlayerExitVehicle(Player player, Vehicle vehicle)
        {
            try
            {
                if (player == null || !player.Exists || vehicle == null || !vehicle.Exists) return;
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnPlayerExitVehicle: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnPlayerExitVehicle: {exception.Message}");
            }
        }
    }
}
