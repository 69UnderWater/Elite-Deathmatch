using System;
using System.Collections.Generic;
using System.Linq;
using Gangwar.DbModels;
using Gangwar.DbModels.Models;
using GTANetworkAPI;

namespace Gangwar.ServerModels
{
    public class ServerFactoryModel : Script
    {
        public static List<Factorys> Factorys_ = new List<Factorys>();
        public static List<ulong> FactoryOnlinePlayers = new List<ulong>();

        public static void AddFactory(Player player)
        {
            Factorys factoryData = new Factorys
            {
                FactoryPosition = player.Position,
                FactoryRobPosition = player.Position,
                FactoryRotation = player.Rotation,
                OwnerId = 1
            };

            Factorys_.Add(factoryData);

            using (gtaContext db = new gtaContext())
            {
                db.Factorys.Add(factoryData);
                db.SaveChanges();
            }
        }

        public static void LoadFactoryPoints()
        {
            foreach(var factoryData in Factorys_)
            {
                var teamData = ServerTeamModel.Teams_.FirstOrDefault(x => x.TeamId == factoryData.OwnerId);
                if (teamData != null)
                {
                    NAPI.Marker.CreateMarker(1, new Vector3(factoryData.FactoryRobPosition.X, factoryData.FactoryRobPosition.Y, factoryData.FactoryRobPosition.Z - 1), new Vector3(0, 0, 0), new Vector3(0, 0, 0), 2f, new Color(0, 255, 0), false, 0);
                    NAPI.Blip.CreateBlip((uint) 478, factoryData.FactoryRobPosition, 1f, (byte)teamData.BlipColor, $"Fabrik: {teamData.TeamName}", shortRange: true);

                    NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_container_03_ld"), new Vector3(factoryData.FactoryRobPosition.X, factoryData.FactoryRobPosition.Y, factoryData.FactoryRobPosition.Z - 1), factoryData.FactoryRotation, dimension: 0);
                    NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_container_03_ld"), new Vector3(factoryData.FactoryRobPosition.X, factoryData.FactoryRobPosition.Y, factoryData.FactoryRobPosition.Z - 1), factoryData.FactoryRotation, dimension: (uint)teamData.TeamId);   
                }
            }
        }
    }
}
