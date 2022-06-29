using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gangwar.DbModels;
using GTANetworkAPI;
using Gangwar.DbModels.Models;
using Gangwar.Objects;

namespace Gangwar.ServerModels
{
    public class ServerFFASpawnModel : Script
    {
        public static List<FFASpawns> FFASpawns_ = new List<FFASpawns>();

        public static void AddFFASpawn(Player player, int arenaId, int maxPlayers, string arenaName)
        {
            try
            {
                if (player == null || !player.Exists) return;

                FFASpawns ffaSpawnData = new FFASpawns
                {
                    ArenaId = arenaId,
                    ArenaName = arenaName,
                    Spawns = new List<FreeForAllObject>(),
                    maxPlayers = maxPlayers,
                    Weapons = new FfaWeaponObject(0, 0, 0)
                };

                FFASpawns_.Add(ffaSpawnData);

                using (gtaContext db = new gtaContext())
                {
                    db.FFASpawns.Add(ffaSpawnData);
                    db.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"AddFFASpawn: {exception.StackTrace}");
                Core.SendConsoleMessage($"AddFFASpawn: {exception.Message}");
            }
        }

        public static void UpdateFfaSpawn(Player player, int arenaId)
        {
            try
            {
                if (player == null || !player.Exists) return;

                var ffaData = ServerFFASpawnModel.FFASpawns_.FirstOrDefault(x => x.ArenaId == arenaId);
                if (ffaData == null) return;

                ffaData.Spawns.Add(new FreeForAllObject(ffaData.Spawns.Count + 1, player.Position, player.Rotation));

                using (gtaContext db = new gtaContext())
                {
                    db.FFASpawns.Update(ffaData);
                    db.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"AddFFASpawn: {exception.StackTrace}");
                Core.SendConsoleMessage($"AddFFASpawn: {exception.Message}");
            }
        }

        public static void UpdateFfaWeapons(Player player, int arenaId, FfaWeaponObject weapons)
        {
            try
            {
                if (player == null || !player.Exists) return;

                var ffaData = ServerFFASpawnModel.FFASpawns_.FirstOrDefault(x => x.ArenaId == arenaId);
                if (ffaData == null) return;

                ffaData.Weapons = weapons;
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"AddFFASpawn: {exception.StackTrace}");
                Core.SendConsoleMessage($"AddFFASpawn: {exception.Message}");
            }
        }

        public static bool DoesFfaArenaExist(int arenaId)
        {
            var ffaSpawnData = FFASpawns_.FirstOrDefault(x => x.ArenaId == arenaId);
            if (ffaSpawnData == null) return false;

            return true;
        }

        public static void GetRandomSpawnPoint(Player player, int arenaId)
        {
            var ffaData = FFASpawns_.FirstOrDefault(x => x.ArenaId == arenaId);
            if (ffaData == null) return;

            int RandomInt = new Random().Next(1, ffaData.Spawns.Count);

            var ffaSpawnData = ffaData.Spawns.FirstOrDefault(x => x.SpawnId == RandomInt);
            if (ffaSpawnData == null) return;

            NAPI.Player.SpawnPlayer(player, ffaSpawnData.Position, ffaSpawnData.Rotation.Z);
        }

        public static void GiveFFAWeapons(Player player, int arenaId)
        {
            var ffaData = FFASpawns_.FirstOrDefault(x => x.ArenaId == arenaId);
            if (ffaData == null) return;

            NAPI.Player.GivePlayerWeapon(player, ffaData.Weapons.WeaponHashOne, 9999);
            NAPI.Player.GivePlayerWeapon(player, ffaData.Weapons.WeaponHashTwo, 9999);
            NAPI.Player.GivePlayerWeapon(player, ffaData.Weapons.WeaponHashThree, 9999);
        }
    }
}
