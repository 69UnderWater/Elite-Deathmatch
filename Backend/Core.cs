using System;
using System.Collections.Generic;
using System.Linq;
using GTANetworkAPI;
using Gangwar.ServerModels;
using Gangwar.DbModels;
using Gangwar.DbModels.Models;
using System.Threading.Tasks;
using Gangwar.Systems.PlayedHoursSystem;
using System.Net;
using static System.Net.WebRequestMethods;
using System.Collections.Specialized;
using Gangwar.Objects;

namespace Gangwar
{
    static class Core
    {
        public static class DatabaseConfig
        {
            public static readonly string Host = "localhost";
            public static readonly string Username = "root";
            public static readonly string Password = "";
            public static readonly string Database = "deathmatch";
            public static readonly string Port = "3306";
        }

        public static class ServerConfig
        {
            public static int WorldTime = 12;
            public static int WorldWeather = 1;
        }

        public static void SendConsoleMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Gangwar] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{message}");
        }

        public static void setFreezed(this Player player, bool freezeState)
        {
            try
            {
                if (player == null || !player.Exists) return;

                player.TriggerEvent("client:freezePlayer", freezeState);
            }
            catch (Exception exception)
            {
                SendConsoleMessage($"setFreezed: {exception.StackTrace}");
                SendConsoleMessage($"setFreezed: {exception.Message}");
            }
        }

        public static void setInvisible(this Player player, bool invisibleState)
        {
            try
            {
                if (player == null || !player.Exists) return;

                player.TriggerEvent("client:setInvisible", invisibleState);
            }
            catch (Exception exception)
            {
                SendConsoleMessage($"setInvisible: {exception.StackTrace}");
                SendConsoleMessage($"setInvisible: {exception.Message}");
            }
        }

        public static void setEnableNametags(this Player player, bool nametagState)
        {
            try
            {
                if (player == null || !player.Exists) return;

                player.TriggerEvent("client:enableNametags", nametagState);
            }
            catch (Exception exception)
            {
                SendConsoleMessage($"setEnableNametags: {exception.StackTrace}");
                SendConsoleMessage($"setEnableNametags: {exception.Message}");
            }
        }

        public static void spectatePlayer(this Player player, Player target, bool state)
        {
            try
            {
                if (player == null || !player.Exists || target == null || !target.Exists) return;

                if (state)
                    player.TriggerEvent("client:spectatePlayer", target);
                else
                    player.TriggerEvent("client:stopSpectating");
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"spectatePlayer: {exception.StackTrace}");
                Core.SendConsoleMessage($"spectatePlayer: {exception.Message}");
            }
        }

        public static int getCurrentTeamId(this Player player)
        {
            try
            {
                if (player == null || !player.Exists) return -1;

                var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return -1;

                return accData.CurrentTeamId;
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"getCurrentTeamId: {exception.StackTrace}");
                Core.SendConsoleMessage($"getCurrentTeamId: {exception.Message}");
            }

            return -1;
        }

        public static void sendMessageToFaction(this Player player, string message)
        {
            if (player == null || !player.Exists) return;

            foreach (var factionPlayers in NAPI.Pools.GetAllPlayers())
            {
                var teamData = ServerTeamModel.Teams_.FirstOrDefault(x => x.TeamId == player.getCurrentTeamId());
                if (teamData == null) return;

                if (teamData.OnlineTeamPlayers.Contains(factionPlayers.SocialClubId))
                {
                    factionPlayers.SendChatMessage($"{message}");
                }
            }
        }

        public static void givePlayerWeapons(this Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;

                Accounts accounts = player.getData();
                if (accounts == null) return;
                
                NAPI.Player.GivePlayerWeapon(player, (uint)accounts.Weapons.WeaponOne, 9999);
                NAPI.Player.GivePlayerWeapon(player, (uint)accounts.Weapons.WeaponTwo, 9999);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage("givePlayerWeapons: " + exception.Message);
                Core.SendConsoleMessage("givePlayerWeapons:" + exception.StackTrace);
            }
        }

        public static void setPlayerHealth(this Player player, int health, int armor)
        {
            try
            {
                if (player == null || !player.Exists) return;
                player.TriggerEvent("Client:AntiCheat:SetHealth", health, armor);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage("givePlayerWeapons: " + exception.Message);
                Core.SendConsoleMessage("givePlayerWeapons:" + exception.StackTrace);
            }
        }

        public static void setPlayerOnlyArmor(this Player player, int armor)
        {
            try
            {
                if (player == null || !player.Exists) return;
                player.TriggerEvent("Client:AntiCheat:SetOnlyArmour", armor);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage("givePlayerWeapons: " + exception.Message);
                Core.SendConsoleMessage("givePlayerWeapons:" + exception.StackTrace);
            }
        }

        public static void sendLanguageToPlayer(this Player player, string messagede, string messageen,
            string messageru)
        {
            if (player == null || !player.Exists) return;

            if (player.HasData("language"))
            {
                switch (player.GetData<int>("language"))
                {
                    case 1:
                        player.SendChatMessage($"{messageen}");
                        break;
                    case 2:
                        player.SendChatMessage($"{messageru}");
                        break;
                    case 3:
                        player.SendChatMessage($"{messagede}");
                        break;
                }
            }
        }

        public static void sendLanguageToEveryone(string messagede, string messageen, string messageru)
        {
            foreach (var players in NAPI.Pools.GetAllPlayers())
            {
                if (players.HasData("language"))
                {
                    switch (players.GetData<int>("language"))
                    {
                        case 1:
                            players.SendChatMessage($"{messageen}");
                            break;
                        case 2:
                            players.SendChatMessage($"{messageru}");
                            break;
                        case 3:
                            players.SendChatMessage($"{messagede}");
                            break;
                    }
                }
            }
        }

        public static void sendAdminMessage(string title, string message)
        {
            foreach (var admins in NAPI.Pools.GetAllPlayers())
            {
                var adminData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == admins.SocialClubId);
                if (adminData == null) return;

                if (adminData.AdminLevel >= Convert.ToInt32(AdminRanksObject.STAFF))
                {
                    admins.SendChatMessage($"[~r~{title}~w~] {message}");
                }
            }
        }

        public static void sendMessageToFaction1(int teamId, string message)
        {
            foreach (var factionPlayers in NAPI.Pools.GetAllPlayers())
            {
                var teamData = ServerTeamModel.Teams_.FirstOrDefault(x => x.TeamId == teamId);
                if (teamData == null) return;

                if (teamData.OnlineTeamPlayers.Contains(factionPlayers.SocialClubId))
                {
                    factionPlayers.SendChatMessage($"{message}");
                }
            }
        }

        [RemoteEvent("Server:MainMenu:SetDailyMissions")]
        public static void OnServerMainMenuSetDailyMissions(Player player)
        {
            try
            {
                var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;

                player.TriggerEvent("client:mainMenuSetDailyMissions",
                    accData.DailyMission.DailyMissionOne.DailyMissionTitle,
                    accData.DailyMission.DailyMissionOne.DailyMissionContent,
                    accData.DailyMission.DailyMissionTwo.DailyMissionTitle,
                    accData.DailyMission.DailyMissionTwo.DailyMissionContent,
                    accData.DailyMission.DailyMissionThree.DailyMissionTitle,
                    accData.DailyMission.DailyMissionThree.DailyMissionContent);
            }
            catch (Exception exception)
            {
                SendConsoleMessage($"OnServerMainMenuSetDailyMissions: {exception.StackTrace}");
                SendConsoleMessage($"OnServerMainMenuSetDailyMissions: {exception.Message}");
            }
        }

        public static void sendwebhook(string url, string Username, string msg)
        {
            Http.Post(url, new NameValueCollection() { { "username", Username }, { "content", msg } });
        }

        public static void StartPlayedHoursTimer(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;

                new Timer(() =>
                {
                    if (player == null || !player.Exists) return;

                    var accData =
                        ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                    if (accData == null) return;

                    PlayedHours.UpdatePlayedHours(player.SocialClubId, 3);
                }, 1000 * 60 * 3, "[Core] Player Hours Timer", 0);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"StartPlayedHoursTimer: {exception.StackTrace}");
                Core.SendConsoleMessage($"StartPlayedHoursTimer: {exception.Message}");
            }
        }

        public static Accounts getData(this Player player)
        {
            Accounts returning = null;
            try
            {
                if (player != null && player.Exists)
                {
                    returning = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                }
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage($"getData: {ex.StackTrace}");
                Core.SendConsoleMessage($"getData: {ex.Message}");
            }

            return returning;
        }

        public static void StartDatabaseUpdateTimer()
        {
            new Timer(() =>
            {
                try
                {
                    using (var db = new gtaContext())
                    {
                        db.Accounts.UpdateRange(ServerAccountModel.Accounts_);
                        db.Vehicles.UpdateRange(ServerAccountModel.Vehicles_);
                        db.FFASpawns.UpdateRange(ServerFFASpawnModel.FFASpawns_);
                        db.AdminRanks.UpdateRange(ServerAdminRankModel.AdminRanks_);
                        db.Teams.UpdateRange(ServerTeamModel.Teams_);
                        db.TeamClothing.UpdateRange(ServerTeamClothingModel.TeamClothing_);
                        db.Factorys.UpdateRange(ServerFactoryModel.Factorys_);
                        db.WeaponTruck.UpdateRange(ServerWeaponTruckModel.WeaponTruck_);
                        db.Inventory.UpdateRange(ServerInventoryModel.Inventory_);
                        db.Dealer.UpdateRange(ServerDealerModel.Dealer_);
                        db.Garage.UpdateRange(ServerTeamGarageModel.Garage_);

                        db.SaveChanges();
                    }
                }
                catch (Exception exception)
                {
                    Core.SendConsoleMessage($"SaveDatabase: {exception.StackTrace}");
                    Core.SendConsoleMessage($"SaveDatabase: {exception.Message}");
                }
            }, 180000, "[Core] Database Save Timer", 0);
        }

        public static void StartCarDespawnTimer()
        {
            new Timer(() =>
            {
                Core.sendLanguageToEveryone(
                    "[~r~SERVER~w~] Es werden in 30 Sekunden alle ungenutzten Fahrzeuge gelöscht.",
                    "[~r~SERVER~w~] All unused vehicles will be deleted in 30 seconds.",
                    "[~r~СЕРВЕР~w~] Все неиспользуемые автомобили будут удалены через 30 секунд.");
                
                new Timer(() =>
                {
                    foreach (var vehicle in NAPI.Pools.GetAllVehicles()
                                 .Where(x => x != null && x.Exists && x.Occupants.Count <= 0 && !x.HasData("weapontruck")))
                    {
                        vehicle.Delete();
                    }

                    Core.sendLanguageToEveryone("[~r~SERVER~w~] Es wurden alle ungenutzten Fahrzeuge gelöscht.",
                        "[~r~SERVER~w~] All unused vehicles were deleted.",
                        "[~r~СЕРВЕР~w~] Все неиспользуемые автомобили были удалены.");
                    
                    StartCarDespawnTimer();
                }, 30000, "[Core] Second Car Despawn Timer", 1);
            }, 1000 * 60 * 10, "[Core] Main Car Despawn Timer", 1);
        }
        
        public static bool IsInRange(this Entity entity, Vector3 position, float range)
            => entity != null && entity.Position.DistanceTo(position) <= range;

        public static bool IsFree(this Vector3 position, float range = 5)
            => NAPI.Pools.GetAllVehicles().FirstOrDefault(x => x.IsInRange(position, range)) == null
               && NAPI.Pools.GetAllPlayers().FirstOrDefault(x => x.IsInRange(position, range)) == null;
    }
}