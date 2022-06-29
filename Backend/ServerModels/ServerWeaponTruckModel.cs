using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Gangwar.DbModels.Models;
using GTANetworkAPI;
using Gangwar.Objects;
using Gangwar.DbModels;

namespace Gangwar.ServerModels
{
    public class ServerWeaponTruckModel : Script
    {
        public static List<WeaponTruck> WeaponTruck_ = new List<WeaponTruck>();

        public static void AddWeaponTruckPosition(WeaponTruckPositionObject spawnPosition, Vector3 returnPosition)
        {
            try
            {
                WeaponTruck weaponTruckData = new WeaponTruck
                {
                    SpawnPosition = spawnPosition,
                    ReturnPosition = returnPosition
                };

                WeaponTruck_.Add(weaponTruckData);

                using (gtaContext db = new gtaContext())
                {
                    db.WeaponTruck.Add(weaponTruckData);
                    db.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"AddWeaponTruckPosition: {exception.StackTrace}");
                Core.SendConsoleMessage($"AddWeaponTruckPosition: {exception.Message}");
            }
        }

        public static WeaponTruckPositionObject GetRandomWeaponTruckSpawnPosition(int Id)
        {
            var weaponTruckData = WeaponTruck_.FirstOrDefault(x => x.Id == Id);
            if (weaponTruckData == null) return new WeaponTruckPositionObject(new Vector3(0, 0, 0), 0f);

            return weaponTruckData.SpawnPosition;
        }

        public static Vector3 GetRandomWeaponTruckReturnPosition(int Id)
        {
            var weaponTruckData = WeaponTruck_.FirstOrDefault(x => x.Id == Id);
            if (weaponTruckData == null) return new Vector3(0, 0, 0);

            return weaponTruckData.ReturnPosition;
        }

        public static void UpdateSpawnPosition(int Id, WeaponTruckPositionObject weaponTruckPositionObject)
        {
            var weaponTruckData = WeaponTruck_.FirstOrDefault(x => x.Id == Id);
            if (weaponTruckData == null) return;

            weaponTruckData.SpawnPosition.Position = weaponTruckPositionObject.Position;
            weaponTruckData.SpawnPosition.Rotation = weaponTruckPositionObject.Rotation;

            using (gtaContext db = new gtaContext())
            {
                db.WeaponTruck.Update(weaponTruckData);
                db.SaveChanges();
            }
        }

        public static void UpdateReturnPosition(int Id, Vector3 Rotation)
        {
            var weaponTruckData = WeaponTruck_.FirstOrDefault(x => x.Id == Id);
            if (weaponTruckData == null) return;

            weaponTruckData.ReturnPosition = Rotation;

            using (gtaContext db = new gtaContext())
            {
                db.WeaponTruck.Update(weaponTruckData);
                db.SaveChanges();
            }
        }
    }
}
