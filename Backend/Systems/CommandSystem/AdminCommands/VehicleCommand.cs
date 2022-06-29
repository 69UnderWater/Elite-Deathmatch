using System;
using System.Collections.Generic;
using System.Linq;
using Gangwar.DbModels.Models;
using GTANetworkAPI;
using Gangwar.Objects;
using Gangwar.ServerModels;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    public class VehicleCommand : Script
    {
        [Command("vehicle", Alias = "veh")]
        public static void VehicleCMD(Player player, string vehicleInput)
        {
            try
            {
                if (player == null || !player.Exists || vehicleInput == " ") return;
                Accounts accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;
                if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.DEVELOPER)) return;

                uint vehicleHash = NAPI.Util.GetHashKey(vehicleInput);
                Vehicle vehicle = NAPI.Vehicle.CreateVehicle(vehicleHash, player.Position, player.Heading, 0, 0, numberPlate: "ADMIN", dimension: player.Dimension);

                NAPI.Task.Run(() =>
                {
                    if (player == null || !player.Exists) return;

                    player.SetIntoVehicle(vehicle, 0);
                }, 250);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"{exception.StackTrace}");
                Core.SendConsoleMessage($"{exception.Message}");
            }
        }
    }
}
