using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gangwar.Systems.EventSystems
{
    public class WeaponComponentLoot : Script
    {
        public static bool isLootDropActive = false;
        public static Timer ComponentLootBoxStartTimer = null;
        public static Timer ComponentLootBoxTimer = null;
        public static GTANetworkAPI.Object ComponentLootBox = null;


        public static void startComponentLootBoxTimer()
        {
            try
            {

            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage("startComponentLootBoxTimer" + exception.Message);
                Core.SendConsoleMessage("startComponentLootBoxTimer" + exception.StackTrace);
            }
            if (isLootDropActive == true) return;

            ComponentLootBoxStartTimer = new Timer(() =>
            {
                startComponentLootBoxEvent();
            }, 1000 * 60 * 45, "[WeaponComponentLoot] Start Timer");
        }

        public static void startComponentLootBoxEvent()
        {

        }
    }
}
