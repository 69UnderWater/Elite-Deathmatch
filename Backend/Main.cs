using System;
using GTANetworkAPI;
using Gangwar.ServerModels;
using Gangwar.DbModels;
using Gangwar.Systems.EventSystems;

namespace Gangwar
{
    public class Main : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            Core.SendConsoleMessage($"Server started...");
            Timer.Init(NAPI.Util.ConsoleOutput, () => (ulong)Environment.TickCount & int.MaxValue);

            NAPI.Server.SetGlobalServerChat(false);
            NAPI.Server.SetAutoSpawnOnConnect(false);
            NAPI.Server.SetAutoRespawnAfterDeath(false);

            Database.LoadDatabase();
            ServerTeamModel.LoadTeams();
            ServerFactoryModel.LoadFactoryPoints();
            WeaponTruck.startWeaponTruckTimer();
            HideAndSeek.startDealerTimer();
            ServerInventoryModel.LoadItems();

            new Timer(() =>
            {
                Console.Clear();
                Core.SendConsoleMessage($"Server ist gestarted...");
                
                Core.StartDatabaseUpdateTimer();
                Core.StartCarDespawnTimer();
            }, 1000, "[Main] Start Timer");
        }

        [ServerEvent(Event.Update)]
        public static void OnUpdateFunc()
        {
            try
            {
                Timer.OnUpdateFunc();
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage("Main.cs:OnUpdateFunc :" + ex.Message);
                Core.SendConsoleMessage("Main.cs:OnUpdateFunc :" + ex.StackTrace);
            }
        }
    }
}
