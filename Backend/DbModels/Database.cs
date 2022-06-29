using System;
using System.Collections.Generic;
using System.Text;
using Gangwar.ServerModels;
using Gangwar.DbModels.Models;

namespace Gangwar.DbModels
{
    public class Database
    {
        public static void LoadDatabase()
        {
            try
            {
                using (gtaContext db = new gtaContext())
                {
                    ServerAccountModel.Accounts_ = new List<Accounts>(db.Accounts);
                    ServerAccountModel.Vehicles_ = new List<Vehicles>(db.Vehicles);
                    ServerFFASpawnModel.FFASpawns_ = new List<FFASpawns>(db.FFASpawns);
                    ServerAdminRankModel.AdminRanks_ = new List<AdminRanks>(db.AdminRanks);
                    ServerTeamModel.Teams_ = new List<Teams>(db.Teams);
                    ServerTeamClothingModel.TeamClothing_ = new List<TeamClothing>(db.TeamClothing);
                    ServerFactoryModel.Factorys_ = new List<Factorys>(db.Factorys);
                    ServerWeaponTruckModel.WeaponTruck_ = new List<WeaponTruck>(db.WeaponTruck);
                    ServerInventoryModel.Inventory_ = new List<Inventory>(db.Inventory);
                    ServerDealerModel.Dealer_ = new List<Dealer>(db.Dealer);
                    ServerTeamGarageModel.Garage_ = new List<Garage>(db.Garage);
                    ServerDeathmatchModel.Deathmatch_ = new List<Deathmatch>(db.Deathmatch);
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"LoadDatabase: {exception.StackTrace}");
                Core.SendConsoleMessage($"LoadDatabase: {exception.Message}");
            }
        }
    }
}
