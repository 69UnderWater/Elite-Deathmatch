using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using Gangwar.DbModels.Models;
using Gangwar.ServerModels;
using System.Linq;
using Gangwar.Objects;
using Gangwar.Objects.MainMenuObjects;
using Gangwar.Systems.RobberySystem;
using Gangwar.Systems.EventSystems;

namespace Gangwar.Systems.KeySystem
{
    public class KeySystem : Script
    {
        [RemoteEvent("Server:E:Event")]
        public static void OnServerEEVEnt (Player player)
        {
            if (player == null || !player.Exists) return;

            Accounts accounts = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
            if (accounts == null) return;

            var factoryData = ServerFactoryModel.Factorys_.FirstOrDefault(x => player.Position.DistanceTo(x.FactoryPosition) <= 2f);
            if(factoryData != null && !player.IsInVehicle && player.Dimension == 0)
            {
                FactoryRob.startFactoryRobbery(player);
                return;
            }

            if(HideAndSeek.DealerLootDrop != null)
            {
                var dealerLootData = HideAndSeek.DealerLootDrop.Position.DistanceTo(player.Position) <= 2;
                if (dealerLootData && player.Dimension == 0 && !player.IsInVehicle && HideAndSeek.DealerLootDrop.Exists)
                {
                    HideAndSeek.getDealerLootFromFloor(player);
                }
            }

            if (HideAndSeek.DealerApplyPed != null)
            {
                var dealerApplyPed = HideAndSeek.DealerApplyPed.Position.DistanceTo(player.Position) <= 2;
                if (dealerApplyPed && player.Dimension == 0 && !player.IsInVehicle && HideAndSeek.DealerApplyPed.Exists)
                {
                    HideAndSeek.submitDealerLoot(player);
                }
            }

            if (HideAndSeek.DealerPed != null)
            {
                var DealerGetPed = HideAndSeek.DealerPed.Position.DistanceTo(player.Position) <= 2;
                if (DealerGetPed && player.Dimension == 0 && !player.IsInVehicle && HideAndSeek.DealerPed.Exists)
                {
                    HideAndSeek.getDealerReward(player);
                }
            }

            var garageData = ServerTeamGarageModel.Garage_.FirstOrDefault(x => x.TeamId == player.getCurrentTeamId() && player.Position.DistanceTo(x.GaragePosition) <= 2.5);
            if (garageData != null)
            {
                List<DisplayVehicleObject> displayVehicleObjects = new List<DisplayVehicleObject>();

                foreach (var vehicles in garageData.Vehicles)
                {
                    displayVehicleObjects.Add(new DisplayVehicleObject(vehicles.Id, vehicles.Name, vehicles.Hash, vehicles.Level));
                }
                
                var garageDataVehicle = new
                {
                    playerData = new PlayerDataObject(accounts.Kills, accounts.Deaths, accounts.Money, accounts.Level, accounts.PlayedHours, true, accounts.SelectedLanguage),
                    vehicles = displayVehicleObjects
                };

                // Show Gfarage
                player.TriggerEvent("Client:Garage:Open", NAPI.Util.ToJson(displayVehicleObjects));
            }
        }

        [RemoteEvent("Server:KeyPress:UseMediKit")]
        public void usePlayerMediKit(Player player)
        {
            try
            {
                if (player == null || !player.Exists || player.IsInVehicle) return;

                NAPI.Player.PlayPlayerAnimation(player, 33, "amb@medic@standing@tendtodead@idle_a", "idle_a", 8f);
                Timer timer = new Timer(() =>
                {
                    if (player == null || !player.Exists || player.IsInVehicle) return;

                    NAPI.Player.StopPlayerAnimation(player);
                }, 4000, "[KeySystem] Medkit Timer");
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage(exception.Message);
                Core.SendConsoleMessage(exception.StackTrace);
            }
        }

        [RemoteEvent("Server:KeyPress:UseVest")]
        public void usePlayerVest (Player player)
        {
            try
            {
                if (player == null || !player.Exists || player.IsInVehicle) return;

                NAPI.Player.PlayPlayerAnimation(player, 33, "anim@heists@narcotics@funding@gang_idle", "gang_chatting_idle01", 8f);
                Timer timer = new Timer(() =>
                {
                    if (player == null || !player.Exists || player.IsInVehicle) return;

                    NAPI.Player.StopPlayerAnimation(player);
                }, 4000, "[KeySystem] Vest Timer");
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage(exception.Message);
                Core.SendConsoleMessage(exception.StackTrace);
            }
        }
    }
}
