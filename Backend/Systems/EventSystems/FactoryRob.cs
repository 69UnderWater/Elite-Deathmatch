using Gangwar.ServerModels;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gangwar.Systems.RobberySystem
{
    public class FactoryRob : Script
    {
        public static DateTime LastFactoryAttackTime = DateTime.Now;
        public static bool IsFactoryAttackedATM = false;
        public static Timer FactoryAttackTimer = null;

        private static int AttackerCount = 0;
        private static int DefenderCount = 0;

        public static void startFactoryRobbery(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;

                var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;

                if (IsFactoryAttackedATM)
                {
                    player.sendLanguageToPlayer($"[~r~FEHLER~w~] Es wird bereits eine andere Fabrik ausgeraubt.",
                        $"[~r~ERROR~w~] Another factory is currently being robbed.",
                        $"[~r~ОШИБКА~w~] В настоящее время ограблен еще один завод.");
                    return;
                }

                if (DateTime.Now < LastFactoryAttackTime)
                {
                    player.sendLanguageToPlayer("[~r~FEHLER~w~] Es wurde vor kurzem erst eine Fabrik ausgeraubt, versuche es später erneut.",
                        "[~r~ERROR~w~] There was a recent factory robbery, try again later.",
                        "[~r~ОШИБКА~w~] Недавно произошло ограбление завода, повторите попытку позже.");
                    return;
                }

                var factoryData = ServerFactoryModel.Factorys_.Find(x => player.Position.DistanceTo(x.FactoryPosition) <= 2f);
                if (factoryData == null) return;

                if (player.getCurrentTeamId() == factoryData.OwnerId)
                {
                    //player.SendChatMessage($"[~r~FEHLER~w~] Du kannst deine eigene Fabrik nicht ausrauben!");
                    player.sendLanguageToPlayer($"[~r~FEHLER~w~] Du kannst deine eigene Fabrik nicht ausrauben!",
                        $"[~r~ERROR~w~] You can not rob your own factory!",
                        $"[~r~ОШИБКА~w~] Вы не можете ограбить свой собственный завод!");
                    return;
                }

                var teamData = ServerTeamModel.Teams_.FirstOrDefault(x => x.TeamId == player.getCurrentTeamId());
                if (teamData == null) return;

                var defTeamData = ServerTeamModel.Teams_.FirstOrDefault(x => x.TeamId == factoryData.OwnerId);
                if (defTeamData == null) return;

                // if (teamData.OnlineTeamPlayers.Count < 1)
                // {
                //     player.sendMessageToFaction($"[~r~FEHLER~w~] In deinem Team sind zuwenig Spieler!");
                //     return;
                // }
                //
                // if (defTeamData.OnlineTeamPlayers.Count < 1)
                // {
                //     player.sendMessageToFaction($"[~r~FEHLER~w~] In dem Gegner-Team sind zuwenig Spieler!");
                //     return;
                // }

                player.SetData("WAS_IN_FACTORYFIGHT", true);
                IsFactoryAttackedATM = true;

                player.sendMessageToFaction($"[~o~FABRIK~w~] Ihr greift eine Fabrik an!");
                Core.sendMessageToFaction1(defTeamData.TeamId, $"[~o~FABRIK~w~] Eure Fabrik wird angegriffen!");
                Core.sendLanguageToEveryone($"[~o~Fabrik~w~] Die Fabrik der ~r~{ServerTeamModel.GetTeamNameById(factoryData.OwnerId)}~w~ wird von den ~r~{ServerTeamModel.GetTeamNameById(player.getCurrentTeamId())} ~w~angegriffen.",
                    $"[~o~Factory~w~] The factory from ~r~{ServerTeamModel.GetTeamNameById(factoryData.OwnerId)}~w~ is attacked by ~r~{ServerTeamModel.GetTeamNameById(player.getCurrentTeamId())}~w~.",
                    $"[~o~Завод~w~] Сайт ~r~{ServerTeamModel.GetTeamNameById(factoryData.OwnerId)} ~w~на завод совершено нападение ~r~{ServerTeamModel.GetTeamNameById(player.getCurrentTeamId())}~w~.");

                ColShape overWorldShape = NAPI.ColShape.CreateSphereColShape(
                    factoryData.FactoryPosition, 1.08f * 100f, 0);
                overWorldShape.SetData("FACTORY_OVERWORLD_SHAPE", true);
                overWorldShape.SetData("FACTORY_OVERWORLD_SHAPE_DEF_ID", defTeamData.TeamId);
                overWorldShape.SetData("FACTORY_OVERWORLD_SHAPE_ATT_ID", teamData.TeamId);
                overWorldShape.SetData("FACTORY_OVERWORLD_SHAPE_DIM_TO", defTeamData.TeamId);

                ColShape factoryShape = NAPI.ColShape.CreateSphereColShape(
                    new Vector3(factoryData.FactoryRobPosition.X, factoryData.FactoryRobPosition.Y, 0), 1.08f * 100f,
                    1);
                ;
                factoryShape.SetData("FACTORY_DIMENSION_SHAPE", true);
                factoryShape.SetData("FACTORY_DIMENSION_SHAPE_DEF_ID", defTeamData.TeamId);
                factoryShape.SetData("FACTORY_DIMENSION_SHAPE_ATT_ID", teamData.TeamId);

                Marker factoryMarker = NAPI.Marker.CreateMarker(1,
                    new Vector3(factoryData.FactoryRobPosition.X, factoryData.FactoryRobPosition.Y, 0),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 2.1f * 100f, new Color(255, 165, 0), false,
                    (uint)defTeamData.TeamId);
                factoryMarker.SetData("FACTORY_MARKER", true);

                FactoryAttackTimer = new Timer(() =>
                {
                    try
                    {
                        overWorldShape = NAPI.Pools.GetAllColShapes().Find(x => x.HasData("FACTORY_OVERWORLD_SHAPE"));
                        factoryShape = NAPI.Pools.GetAllColShapes().Find(x => x.HasData("FACTORY_DIMENSION_SHAPE"));
                        factoryMarker = NAPI.Pools.GetAllMarkers().Find(x => x.HasData("FACTORY_MARKER"));

                        var teamData = ServerTeamModel.Teams_.FirstOrDefault(x => x.TeamId == overWorldShape.GetData<int>("FACTORY_OVERWORLD_SHAPE_ATT_ID"));
                        if (teamData == null) return;

                        var defTeamData = ServerTeamModel.Teams_.FirstOrDefault(x => x.TeamId == overWorldShape.GetData<int>("FACTORY_OVERWORLD_SHAPE_DEF_ID"));
                        if (defTeamData == null) return;

                        foreach (var players in NAPI.Pools.GetAllPlayers())
                        {
                            if (players != null && players.Exists)
                            {
                                if (players.HasData("WAS_IN_FACTORYFIGHT"))
                                {
                                    players.ResetData("WAS_IN_FACTORYFIGHT");
                                }
                            
                                var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == players.SocialClubId);
                                if (accData != null)
                                {
                                    if (players.getCurrentTeamId() == teamData.TeamId && accData.IsFactoryFight)
                                        AttackerCount++;
                                    else if (players.getCurrentTeamId() == defTeamData.TeamId && accData.IsFactoryFight)
                                        DefenderCount++;

                                    if (accData.IsFactoryFight)
                                    {
                                        NAPI.Player.SpawnPlayer(players,
                                            ServerTeamModel.GetTeamSpawnPosition(players.getCurrentTeamId()),
                                            ServerTeamModel.GetTeamSpawnRotation(players.getCurrentTeamId()).Z);
                                        player.Dimension = 0;
                                        NAPI.Entity.SetEntityDimension(players, 0);
                                        ServerFactoryModel.FactoryOnlinePlayers.Clear();

                                        NAPI.Task.Run(() =>
                                        {
                                            accData.IsFactoryFight = false;
                                        }, 100);
                                    }   
                                }
                            }
                        }

                        overWorldShape.Delete();
                        factoryShape.Delete();
                        factoryMarker.Delete();

                        LastFactoryAttackTime = DateTime.Now.AddMinutes(30);
                        IsFactoryAttackedATM = false;

                        if (AttackerCount > DefenderCount)
                        {
                            // Angreifer gewinnen = Angreifer reward
                            Core.sendMessageToFaction1(teamData.TeamId, $"[~o~FABRIK~w~] Ihr hab die Fabrik erfolgreich ausgeraubt.");
                            Core.sendMessageToFaction1(teamData.TeamId, $"[~o~FABRIK~w~] Ihr habt Items erhalten!");
                            foreach (var players in NAPI.Pools.GetAllPlayers().FindAll(x => x.getCurrentTeamId() == teamData.TeamId))
                            {
                                if (players != null && players.Exists)
                                {
                                    ServerInventoryModel.AddItem(players, "XPToken");
                                    ServerInventoryModel.AddItem(players, "Geldsack"); 
                                }
                            }
                        }
                        else if (AttackerCount < DefenderCount)
                        {
                            // Defender gewinnen = Defender reward
                            Core.sendMessageToFaction1(defTeamData.TeamId, $"[~o~FABRIK~w~] Ihr hab die Fabrik erfolgreich verteidigt.");
                            Core.sendMessageToFaction1(defTeamData.TeamId, $"[~o~FABRIK~w~] Ihr habt Items erhalten!");
                            foreach (var players in NAPI.Pools.GetAllPlayers().FindAll(x => x.getCurrentTeamId() == defTeamData.TeamId))
                            {
                                if (players != null && players.Exists)
                                {
                                    ServerInventoryModel.AddItem(players, "XPToken");
                                    ServerInventoryModel.AddItem(players, "Geldsack");   
                                }
                            }
                        }
                        else if (AttackerCount == DefenderCount)
                        {
                            // Unendschieden = Beide reward
                            Core.sendMessageToFaction1(teamData.TeamId, $"[~o~FABRIK~w~] Unentschieden. Die Fabrik wurde weder verteidigt noch ausgeraubt");
                            Core.sendMessageToFaction1(defTeamData.TeamId, $"[~o~FABRIK~w~] Unentschieden. Die Fabrik wurde weder verteidigt noch ausgeraubt");
                            Core.sendMessageToFaction1(teamData.TeamId, $"[~o~FABRIK~w~] Ihr habt Items erhalten!");
                            Core.sendMessageToFaction1(defTeamData.TeamId, $"[~o~FABRIK~w~] Ihr habt Items erhalten!");
                            foreach (var players in NAPI.Pools.GetAllPlayers().FindAll(x => x.getCurrentTeamId() == teamData.TeamId || x.getCurrentTeamId() == defTeamData.TeamId))
                            {
                                if (players != null && players.Exists)
                                {
                                    ServerInventoryModel.AddItem(players, "XPToken");
                                    ServerInventoryModel.AddItem(players, "Geldsack");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Core.SendConsoleMessage($"startFactoryRobbery Timer: {ex.Message}");
                        Core.SendConsoleMessage($"startFactoryRobbery Timer: {ex.StackTrace}");
                    }
                }, 1000 * 60 * 10, "[FactoryRob] Fatory Attak Timer");
            }
            catch (Exception ex)
            {
                Core.SendConsoleMessage($"startFactoryRobbery: {ex.Message}");
                Core.SendConsoleMessage($"startFactoryRobbery: {ex.StackTrace}");
            }
        }
    }
}
