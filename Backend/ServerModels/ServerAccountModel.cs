using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gangwar.DbModels;
using GTANetworkAPI;
using Gangwar.DbModels.Models;
using Gangwar.Objects;
using Gangwar.Objects.MainMenuObjects;
using Gangwar.Objects.WeaponObject;

namespace Gangwar.ServerModels
{
    public class ServerAccountModel : Script
    {
        public static List<Accounts> Accounts_ = new List<Accounts>();
        public static List<Vehicles> Vehicles_ = new List<Vehicles>();

        public static async Task AddAccount(Player player, string username)
        {
            try
            {
                if (player == null || !player.Exists) return;

                await Task.Run(() =>
                {
                    Accounts accData = new Accounts
                    {
                        Username = username,
                        SocialclubId = player.SocialClubId,
                        SocialclubName = player.SocialClubName,
                        HardwareId = player.Serial,
                        AdminLevel = 0,
                        Level = 1,
                        Prestige = 0,
                        CurrentXP = 0,
                        Kills = 0,
                        Deaths = 0,
                        Money = 0,
                        PlayedHours = 0,
                        SelectedLanguage = 3,
                        PrivateFrakId = 0,
                        PrivateFrakRank = 0,
                        IsPrivateFrak = false,
                        Weapons = new WeaponObject(-1716589765, 1649403952),
                        IsBanned = false,
                        IsMuted = false,
                        IsTimeBanned = false,
                        BanDate = DateTime.Now,
                        TimeBanUntil = DateTime.Now,
                        Warns = 0,
                        BanReason = "",
                        guildMemberId = "",
                        discordSyncCode = "",
                        DailyMission = new DailyMissionObject(new DailyMissonContentObject(1, "Test 1", "Test 1"),
                            new DailyMissonContentObject(2, "Test 2", "Test 2"),
                            new DailyMissonContentObject(3, "Test 3", "Test 3"), DateTime.Now)
                    };

                    Accounts_.Add(accData);

                    NAPI.Task.Run(() =>
                    {
                        if (player == null || !player.Exists) return;

                        LoadAccount(player);
                    }, 1000);
                });
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"AddAccount: {exception.StackTrace}");
                Core.SendConsoleMessage($"AddAccount: {exception.Message}");
            }
        }

        public static async Task LoadAccount(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;

                await Task.Run(() =>
                {
                    var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                    if (accData == null) return;

                    player.Name = $"{accData.Username}";
                    NAPI.Player.SetPlayerName(player, $"{accData.Username}");
                    player.TriggerEvent("Client:AntiCheat:SetHealth", 100, 100);

                    accData.IsAduty = false;
                    player.SetData<int>("language", accData.SelectedLanguage);

                    NAPI.Task.Run(() =>
                    {
                        player.Name = $"{accData.Username}";
                        NAPI.Player.SetPlayerName(player, $"{accData.Username}");
                    }, 1000);
                });
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"LoadAccount: {exception.StackTrace}");
                Core.SendConsoleMessage($"LoadAccount: {exception.Message}");
            }
        }

        public static void UpdatePlayerKills(Player player)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
            if (accData == null) return;

            accData.Kills += 1;

            var playerData = new PlayerDataObject(accData.Kills, accData.Deaths, accData.Money, accData.Level,
                accData.PlayedHours, true, accData.SelectedLanguage);

            player.TriggerEvent("client:updateHud", accData.Kills, accData.Deaths, accData.Level);
            player.TriggerEvent("client:updatePlayerMainMenu", NAPI.Util.ToJson(playerData));
        }

        public static void SetPlayerMoney(Player player, int money)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
            if (accData == null) return;

            accData.Money += money;
        }

        public static void UpdatePlayerDeaths(Player player)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
            if (accData == null) return;

            accData.Deaths += 1;

            player.TriggerEvent("client:updateHud", accData.Kills, accData.Deaths, accData.Level);
        }

        public static string RandomString()
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 8)
                .Select(s => s[new Random().Next(s.Length)])
                .ToArray());
        }

        public static bool DoesTokenExist(string token)
        {
            var accData = Accounts_.FirstOrDefault(x => x.discordSyncCode == token);
            if (accData == null) return false;

            return true;
        }

        public static void UpdateToken(ulong socialClubId, string token)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == socialClubId);
            if (accData == null) return;

            accData.discordSyncCode = token;
        }

        public static bool CheckHardwareId(string hardwareId)
        {
            var accData = Accounts_.FirstOrDefault(x => x.HardwareId == hardwareId);
            if (accData == null) return false;

            return true;
        }

        public static bool CheckLoginDetails(ulong socialClubId, string username)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == socialClubId);
            if (accData == null) return false;

            if (accData.Username == username) return true;

            return false;
        }

        public static bool CheckSocialclubId(ulong socialClubId)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == socialClubId);
            if (accData == null) return false;

            return true;
        }

        public static string GetUsernameBySocialClubId(ulong socialClubId)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == socialClubId);
            if (accData == null) return "";

            return accData.Username;
        }

        public static bool DoesUsernameExist(string username)
        {
            var accData = Accounts_.FirstOrDefault(x => x.Username == username);
            if (accData == null) return false;

            return true;
        }

        public static bool IsPlayerBanned(ulong socialClubId)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == socialClubId);
            if (accData == null) return false;

            if (accData.IsBanned)
            {
                return true;
            }
            else if (accData.IsTimeBanned)
            {
                if (accData.TimeBanUntil <= DateTime.Now)
                {
                    accData.IsBanned = false;
                    accData.IsTimeBanned = false;
                    accData.BanReason = "";

                    return false;
                }
                
                return true;
            }

            return false;
        }

        public static bool HasAccount(ulong socialClubId)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == socialClubId);
            if (accData == null) return false;

            return true; 
        }
        
        public static int GetPlayerLanguage(ulong socialClubId)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == socialClubId);
            if (accData == null) return -1;

            return accData.SelectedLanguage;
        }

        public static void SetPlayerBanned(Player player, bool isBanned)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
            if (accData == null) return;

            accData.IsBanned = isBanned;
            accData.BanDate = DateTime.Now;
        }

        public static void SetPlayerMuted(Player player, bool isMuted)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
            if (accData == null) return;

            accData.IsMuted = isMuted;
        }

        public static DateTime? GetPlayerBanStamp(Player player)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
            if (accData == null) return null;

            return accData.BanDate;
        }

        public static DateTime? GetPlayerTimebanUntil(Player player)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
            if (accData == null) return null;

            return accData.TimeBanUntil;
        }

        public static void SetPlayerTimeBan(Player player, double timeBan)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
            if (accData == null) return;

            accData.TimeBanUntil = DateTime.Now.AddHours(timeBan);
        }

        public static void SetPlayerTimeBanned(Player player, bool timeBanned)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
            if (accData == null) return;

            accData.IsTimeBanned = timeBanned;
        }

        public static void UpdateMoney(Player player, int money)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
            if (accData == null) return;

            accData.Money += money;
        }

        public static void AddVehicleAccount(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;

                Accounts accData = player.getData();
                if (accData == null) return;

                Vehicles vehData = new Vehicles
                {
                    AccountId = accData.Id, PrivateVehicles = new List<GarageVehicleObject>()
                };

                Vehicles_.Add(vehData);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"AddAccount: {exception.StackTrace}");
                Core.SendConsoleMessage($"AddAccount: {exception.Message}");
            }
        }

        public static void SetPlayerEXP(ulong socialClubId, int XP)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == socialClubId);
            if (accData == null) return;

            accData.CurrentXP = XP;
        }

        public static int GetPlayerEXP(ulong socialClubId)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == socialClubId);
            if (accData == null) return -1;

            return accData.CurrentXP;
        }

        public static int GetPlayerLevel(ulong socialClubId)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == socialClubId);
            if (accData == null) return -1;

            return accData.Level;
        }

        public static void SetPlayerLevel(ulong socialClubId, int Level)
        {
            var accData = Accounts_.FirstOrDefault(x => x.SocialclubId == socialClubId);
            if (accData == null) return;

            accData.Level = Level;
        }
    }
}