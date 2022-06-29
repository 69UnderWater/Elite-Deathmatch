using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GTANetworkAPI;
using Gangwar.ServerModels;
using Gangwar.Objects;
using Gangwar.DbModels;
using Gangwar.DbModels.Models;

namespace Gangwar.ServerModels
{
    public class ServerTeamGarageModel : Script
    {
        public static List<Garage> Garage_ = new List<Garage>();

        public static void AddGarageParkOutPoint(Player player, int teamId)
        {
            try
            {
                if (player == null || !player.Exists) return;

                var garageData = ServerTeamGarageModel.Garage_.FirstOrDefault(x => x.TeamId == teamId);
                if (garageData == null) return;

                garageData.ParkoutPoints.Add(new ParkoutPointObject(player.Position, player.Rotation));

                using (gtaContext db = new gtaContext())
                {
                    db.Garage.Update(garageData);
                    db.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"AddGarage: {exception.StackTrace}");
                Core.SendConsoleMessage($"AddGarage: {exception.Message}");
            }
        }
        
        public static void AddVehicle(Player player, int teamId, int Id, string name, string hash, int level)
        {
            try
            {
                if (player == null || !player.Exists) return;

                var garageData = ServerTeamGarageModel.Garage_.FirstOrDefault(x => x.TeamId == teamId);
                if (garageData == null) return;

                garageData.Vehicles.Add(new GarageVehicleObject(Id, name, hash, level));

                using (gtaContext db = new gtaContext())
                {
                    db.Garage.Update(garageData);
                    db.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"AddGarage: {exception.StackTrace}");
                Core.SendConsoleMessage($"AddGarage: {exception.Message}");
            }
        }

        public static bool IsAtGarage(Player player)
        {
            var garageData = Garage_.FirstOrDefault(x => player.Position.DistanceTo(x.GaragePosition) <= 2f);
            if (garageData == null) return false;

            return true;
        }
    }
}
