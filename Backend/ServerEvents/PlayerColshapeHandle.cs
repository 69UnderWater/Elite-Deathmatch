using Gangwar.ServerModels;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Gangwar.DbModels.Models;
using Gangwar.Systems.EventSystems;

namespace Gangwar.Systems.ColShapeSystem
{
    public class PlayerColshapeHandle : Script
    {
        [ServerEvent(Event.PlayerEnterColshape)]
        public void OnPlayerEnterColshape(ColShape shape, Player player)
        {
            try
            {
                if (player == null || !player.Exists || shape == null) return;

                if (shape.HasData("WeaponTruckEventShape") && player.Dimension == 0 && player.IsInVehicle && player.Vehicle.HasData("weapontruck"))
                {
                    //NAPI.Chat.SendChatMessageToAll($"[~r~EVENT~w~] Die Fraktion {ServerTeamModel.GetTeamNameById(player.getCurrentTeamId())} hat den Waffentruck erfoglreich abgegeben.");
                    Core.sendLanguageToEveryone($"[~r~EVENT~w~] Die Fraktion {ServerTeamModel.GetTeamNameById(player.getCurrentTeamId())} hat den Waffentruck erfoglreich abgegeben.",
                        $"[~r~EVENT~w~] The faction {ServerTeamModel.GetTeamNameById(player.getCurrentTeamId())} has delivered the weapontruck.",
                        $"[~r~СОБЫТИЕ~w~] Фракция {ServerTeamModel.GetTeamNameById(player.getCurrentTeamId())} доставил оружие.");

                    foreach (var players in NAPI.Pools.GetAllPlayers().FindAll(x => x.HasSharedData("PLAYER_TEAM_ID") && x.GetSharedData<int>("PLAYER_TEAM_ID") == player.getCurrentTeamId() && player.Position.DistanceTo(x.Position) <= 30))
                    {
                        if (players != player)
                        {
                            //players.SendChatMessage($"[~r~EVENT~w~] Da du {player.Name} bei der Abgabe des Waffentrucks geholfen hast, erhälst du: XYZ"); //ToDo: Give something
                            players.sendLanguageToPlayer($"[~r~EVENT~w~] Da du {player.Name} bei der Abgabe des Waffentrucks geholfen hast, erhälst du: 1x XP-Token",
                                $"[~r~EVENT~w~] Because you helped {player.Name} to deliver the weapon truck, you received: 1x XP-Token",
                                $"[~r~СОБЫТИЕ~w~] Поскольку вы помогли {player.Name} доставить грузовик с оружием, вы получили: 1x XP-Token");
                            ServerInventoryModel.AddItem(players, "XPToken", 1);

                        }
                    }

                    player.sendLanguageToPlayer($"[~r~EVENT~w~] Da du denn Waffentruck abgegeben hast erhälst du: 1x XP-Token",
                        "[~r~EVENT~w~] Since you handed out the gun truck, you get: 1x XP-Token",
                        "[~r~EVENT~w~] Поскольку вы раздавали грузовик с оружием, вы получаете: 1x XP-Token");

                    ServerInventoryModel.AddItem(player, "XPToken", 1);

                    WeaponTruck.WeaponTruckTimer.Kill();
                    WeaponTruck.WeaponTruckBlipTimer.Kill();
                    WeaponTruck.TruckEventBlip.Delete();
                    WeaponTruck.TruckApplyShape.Delete();
                    WeaponTruck.WeaponTruckVehicle.Delete();
                    WeaponTruck.TruckReturnBlip.Delete();

                    NAPI.Task.Run(() =>
                    {
                        WeaponTruck.isTruckActive = false;
                        WeaponTruck.WeaponTruckStartTimer = null;
                        WeaponTruck.WeaponTruckTimer = null;
                        WeaponTruck.WeaponTruckBlipTimer = null;
                        WeaponTruck.TruckApplyShape = null;
                        WeaponTruck.TruckEventBlip = null;
                        WeaponTruck.WeaponTruckVehicle = null;
                        WeaponTruck.TruckReturnBlip = null;

                        WeaponTruck.startWeaponTruckTimer();
                    }, 100);
                }

                if (shape.HasData("FACTORY_OVERWORLD_SHAPE") && player.Dimension == 0)
                {
                    if (player.HasData("WAS_IN_FACTORYFIGHT"))
                    {
                        player.sendLanguageToPlayer("[~o~FABRIK~w~] Du kannst nur einmal Teilnehmen", "[~o~FABRIK~w~] You only can enter once", "[~o~FABRIK~w~] Вы можете войти только один раз");
                    }
                    if (player.getCurrentTeamId() == shape.GetData<int>("FACTORY_OVERWORLD_SHAPE_DEF_ID") ||
                        player.getCurrentTeamId() == shape.GetData<int>("FACTORY_OVERWORLD_SHAPE_ATT_ID"))
                    {
                        var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                        if (accData == null) return;

                        var factoryData = ServerFactoryModel.Factorys_.FirstOrDefault(x => x.OwnerId == shape.GetData<int>("FACTORY_OVERWORLD_SHAPE_DEF_ID"));
                        if (factoryData == null) return;

                        if (ServerFactoryModel.FactoryOnlinePlayers.Contains(player.SocialClubId)) return;
                        
                        if (player.IsInVehicle)
                        {
                            player.Dimension = (uint)shape.GetData<int>("FACTORY_OVERWORLD_SHAPE_DIM_TO");
                            player.Vehicle.Dimension = (uint)shape.GetData<int>("FACTORY_OVERWORLD_SHAPE_DIM_TO");
                        }

                        player.Dimension = (uint)shape.GetData<int>("FACTORY_OVERWORLD_SHAPE_DIM_TO");
                        accData.IsFactoryFight = true;

                        ServerFactoryModel.FactoryOnlinePlayers.Add(player.SocialClubId);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage("OnPlayerEnterColshape: " + ex.Message);
                Core.SendConsoleMessage("OnPlayerEnterColshape: " + ex.StackTrace);
            }
        }

        [ServerEvent(Event.PlayerExitColshape)]
        public void OnPlayerExitColShape(ColShape shape, Player player)
        {
            if (player == null || !player.Exists || shape == null) return;

            if (shape.HasData("FACTORY_DIMENSION_SHAPE") && player.Dimension == shape.Dimension)
            {
                var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;

                player.Dimension = 0;

                if (player.IsInVehicle)
                {
                    player.Vehicle.Dimension = 0;
                }

                accData.IsFactoryFight = false;

                player.sendLanguageToPlayer("[~o~FABRIK~w~] Du hast die Fabrik verlassen und kannst nun nicht mehr Teilnehmen", "[~o~FABRIK~w~] You have left the Factory and cannot enter again", "[~o~FABRIK~w~] Вы покинули Фабрику и не можете войти снова");
            }
        }
    }
}
