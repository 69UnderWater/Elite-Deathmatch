using System;
using System.Collections.Generic;
using System.Linq;
using GTANetworkAPI;
using Gangwar.DbModels.Models;
using Gangwar.Objects;

namespace Gangwar.ServerModels
{
    public class ServerAdminRankModel : Script
    {
        public static List<AdminRanks> AdminRanks_ = new List<AdminRanks>();

        public static void AddAdminRank(string adminRank, int permissionLevel, string chatColor)
        {
            try
            {
                AdminRanks adminRanks = new AdminRanks
                {
                    Name = adminRank,
                    Permission = permissionLevel,
                    ChatColor = chatColor,
                    AdminClothing = new AdminClothingObject(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
                };

                AdminRanks_.Add(adminRanks);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"AddAdminRank: {exception.StackTrace}");
                Core.SendConsoleMessage($"AddAdminRank: {exception.Message}");
            }
        }

        public static void LoadAdminClothing(Player player, string adminRankName)
        {
            try
            {
                var adminRank = AdminRanks_.FirstOrDefault(x => x.Name == adminRankName);
                if (adminRank == null) return;

                player.SetClothes(1, adminRank.AdminClothing.MaskDrawable, adminRank.AdminClothing.MaskTexture);
                player.SetClothes(3, adminRank.AdminClothing.TorsoDrawable, adminRank.AdminClothing.TorsoTexture);
                player.SetClothes(4, adminRank.AdminClothing.LegsDrawable, adminRank.AdminClothing.LegsTexture);
                player.SetClothes(6, adminRank.AdminClothing.ShoeDrawable, adminRank.AdminClothing.ShoeTexture);
                player.SetClothes(8, adminRank.AdminClothing.UndershirtDrawable, adminRank.AdminClothing.UndershirtTexture);
                player.SetClothes(11, adminRank.AdminClothing.TopDrawable, adminRank.AdminClothing.TopTexture);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"LoadAdminClothing: {exception.StackTrace}");
                Core.SendConsoleMessage($"LoadAdminClothing: {exception.Message}");
            }
        }

        public static void UpdateAdminClothing(Player player, string adminRankName)
        {
            var adminRank = AdminRanks_.FirstOrDefault(x => x.Name == adminRankName);
            if (adminRank == null) return;

            adminRank.AdminClothing.MaskTexture = player.GetClothesTexture(1);
            adminRank.AdminClothing.MaskDrawable = player.GetClothesDrawable(1);
            adminRank.AdminClothing.TorsoTexture = player.GetClothesTexture(3);
            adminRank.AdminClothing.TorsoDrawable = player.GetClothesDrawable(3);
            adminRank.AdminClothing.LegsTexture = player.GetClothesTexture(4);
            adminRank.AdminClothing.LegsDrawable = player.GetClothesDrawable(4);
            adminRank.AdminClothing.ShoeTexture = player.GetClothesTexture(6);
            adminRank.AdminClothing.ShoeDrawable = player.GetClothesDrawable(6);
            adminRank.AdminClothing.UndershirtTexture = player.GetClothesTexture(8);
            adminRank.AdminClothing.UndershirtDrawable = player.GetClothesDrawable(8);
            adminRank.AdminClothing.TopTexture = player.GetClothesTexture(11);
            adminRank.AdminClothing.TopDrawable = player.GetClothesDrawable(11);
        }

        public static int GetPermissionLevel(string adminRankName)
        {
            var adminRank = AdminRanks_.FirstOrDefault(x => x.Name == adminRankName);
            if (adminRank == null) return -1;

            return adminRank.Permission;
        }

        public static string GetChatColor(string adminRankName)
        {
            var adminRank = AdminRanks_.FirstOrDefault(x => x.Name == adminRankName);
            if (adminRank == null) return "undefined";

            return adminRank.ChatColor;
        }

        public static string GetAdminRankName(int permissionLevel)
        {
            var adminRank = AdminRanks_.FirstOrDefault(x => x.Permission == permissionLevel);
            if (adminRank == null) return "undefined";

            return adminRank.Name;
        }
    }
}
