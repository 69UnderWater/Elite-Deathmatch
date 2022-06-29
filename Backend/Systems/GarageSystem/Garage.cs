using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gangwar;
using Gangwar.Objects;
using Gangwar.ServerModels;
using GTANetworkAPI;

namespace Gangwar.Systems.GarageSystem
{
    public class Garage : Script
    {
        [RemoteEvent("Server:Garage:ParkOut")]
        public void ParkOut(Player player, string hash, int level)
        {
            try
            {
                if (player == null || !player.Exists) return;

                var accData = player.getData();
                if (accData == null) return;

                var garageData = ServerTeamGarageModel.Garage_.FirstOrDefault(x => x.TeamId == player.getCurrentTeamId());
                if (garageData == null) return;

                var teamData = ServerTeamModel.Teams_.FirstOrDefault(x => x.TeamId == player.getCurrentTeamId());
                if (teamData == null) return;

                if (accData.Level < level)
                {
                    player.TriggerEvent("Client:Garage:Close");
                    player.Position = teamData.TeamSpawnPoint;
                    player.sendLanguageToPlayer("[~r~SERVER~w~] Dein Level reicht für dieses Fahrzeug nicht.", "[~r~SERVER~w~] Your level is not high enough for this vehicle.", "[~r~СЕРВЕР~w~] Ваш уровень недостаточно высок для данного транспортного средства.");
                    return;
                }

/*                if (player.HasData("PLAYER_VEHICLE"))
                {
                    if (player.GetData<Vehicle>("PLAYER_VEHICLE") != null && player.GetData<Vehicle>("PLAYER_VEHICLE").Exists)
                    {
                        player.GetData<Vehicle>("PLAYER_VEHICLE").Delete();
                        player.ResetData("PLAYER_VEHICLE");   
                    }
                }*/
                player.Dimension = 0;
                player.Position = garageData.GaragePosition;

                if (NAPI.Pools.GetAllVehicles().Find(x => garageData.GarageOutParkPoint.DistanceTo(x.Position) <= 2f) == null)
                {
                    foreach (var positions in garageData.ParkoutPoints)
                    {
                        if (positions.Position.IsFree())
                        {
                            Vehicle veh = NAPI.Vehicle.CreateVehicle((VehicleHash)NAPI.Util.GetHashKey(hash),
                                positions.Position,
                                positions.Rotation.Z,
                                new Color(teamData.PrimaryColor.r, teamData.PrimaryColor.g, teamData.PrimaryColor.b),
                                new Color(teamData.SecondaryColor.r, teamData.SecondaryColor.g, teamData.SecondaryColor.b), dimension:0);
                            NAPI.Task.Run(() =>
                            {
                                if (veh == null || !veh.Exists) return;
                                NAPI.Vehicle.SetVehicleCustomPrimaryColor(veh, teamData.PrimaryColor.r, teamData.PrimaryColor.g,
                                    teamData.PrimaryColor.b);
                                NAPI.Vehicle.SetVehicleCustomSecondaryColor(veh, teamData.SecondaryColor.r, teamData.SecondaryColor.g,
                                    teamData.SecondaryColor.b);
                                player.SetIntoVehicle(veh, 0);
                            }, 250);
                            player.TriggerEvent("Client:Garage:Close");
                            break;
                        }
                        else
                        {
                            player.sendLanguageToPlayer($"kein punkt frei", "kein freier punkt", "kein punkt ist frei");
                            player.TriggerEvent("Client:Garage:Close");
                        }
                    }
                    //player.SetData("PLAYER_VEHICLE", veh);
                }
                else
                {
                    Vehicle targetVeh = NAPI.Pools.GetAllVehicles()
                        .Find(x => garageData.GarageOutParkPoint.DistanceTo(x.Position) <= 2f);
                    if (targetVeh == null || !targetVeh.Exists) return;
                    if (targetVeh.Occupants.Count <= 0)
                    {
                        targetVeh.Delete();
                    }
                    else
                    {
                        player.TriggerEvent("Client:Garage:Close");
                        player.sendLanguageToPlayer("[~r~SERVER~w~] Aktuell gibt es keine freien Ausparkpunkte.", "[~r~SERVER~w~] There are currently no free parking spaces.", "[~r~СЕРВЕР~w~] В настоящее время нет бесплатных парковочных мест");
                        return;
                    }
                    
                    player.TriggerEvent("Client:Garage:Close");
                }
                
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage($"ParkOut: {ex.StackTrace}");
                Core.SendConsoleMessage($"ParkOut: {ex.Message}");
            }
        }
    }
}
