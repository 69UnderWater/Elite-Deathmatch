using System;
using System.Linq;
using Gangwar.ServerModels;
using Gangwar.Systems.EventSystems;
using Gangwar.Systems.OneVsOneSystem;
using GTANetworkAPI;

namespace Gangwar.ServerEvents
{
    public class PlayerDeathHandle : Script
    {
        [RemoteEvent("PlayerDeathEvent")]
        public static void OnCustomPlayerDeath(Player player, String targetplayer)
        {
            try
            {
                Player killer = NAPI.Player.GetPlayerFromName(targetplayer);
                if (player == null || !player.Exists) return;

                var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;

                if (killer != null && killer.Exists)
                {
                    var killerData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == killer.SocialClubId);
                    if (killerData == null) return;

                    player.sendLanguageToPlayer($"[~r~DEATH~w~] {killerData.Username} hat dich getötet. [{killer.Health + killer.Armor} HP]", $"[~r~DEATH~w~] {killerData.Username} has killed you. [{killer.Health + killer.Armor} HP]", $"[~r~DEATH~w~] {killerData.Username} убил тебя. [{killer.Health + killer.Armor} HP]");
                    killer.sendLanguageToPlayer($"[~r~KILL~w~] Du hast {accData.Username} getötet", $"[~r~KILL~w~] You have killed {accData.Username}", $"[~r~KILL~w~] Ты убил {accData.Username}");

                    if (player.GetSharedData<bool>("IS_IN_FFA") == true && killer.GetSharedData<bool>("IS_IN_FFA") == true)
                    {
                        ServerAccountModel.UpdatePlayerDeaths(player);
                        ServerAccountModel.UpdatePlayerKills(killer);
                        killer.setPlayerHealth(100, 100);
                        NAPI.Task.Run(() =>
                        {
                            try
                            {
                                if (player == null || !player.Exists) return;
                                
                                ServerFFASpawnModel.GetRandomSpawnPoint(player, player.GetSharedData<int>("PLAYER_ARENA_ID"));
                                ServerFFASpawnModel.GiveFFAWeapons(player, player.GetSharedData<int>("PLAYER_ARENA_ID"));
                            }
                            catch (Exception ex)
                            {
                                Core.SendConsoleMessage($"OnCustomPlayerDeath FFA Respawn: {ex.StackTrace}");
                                Core.SendConsoleMessage($"OnCustomPlayerDeath FFA Respawn: {ex.Message}");
                            }
                        }, 2500);

                        ServerAccountModel.SetPlayerEXP(killer.SocialClubId, ServerAccountModel.GetPlayerEXP(killer.SocialClubId) + 2);

                        if (ServerAccountModel.GetPlayerEXP(killer.SocialClubId) >= ServerAccountModel.GetPlayerLevel(killer.SocialClubId) * 25)
                        {
                            ServerAccountModel.SetPlayerEXP(killer.SocialClubId, 0);
                            ServerAccountModel.SetPlayerLevel(killer.SocialClubId, ServerAccountModel.GetPlayerLevel(killer.SocialClubId) + 1);
                        }
                    }
                    if (accData.IsFactoryFight)
                    {
                        ServerAccountModel.UpdatePlayerDeaths(player);
                        ServerAccountModel.UpdatePlayerKills(killer);

                        accData.IsFactoryFight = false;

                        NAPI.Task.Run(() =>
                        {
                            try
                            {
                                if (player == null || !player.Exists) return;
                                player.Dimension = 0;
                                NAPI.Entity.SetEntityDimension(player, 0);
                                NAPI.Player.SpawnPlayer(player, ServerTeamModel.GetTeamSpawnPosition(player.getCurrentTeamId()), ServerTeamModel.GetTeamSpawnRotation(player.getCurrentTeamId()).Z);
                            }
                            catch (Exception ex)
                            {
                                Core.SendConsoleMessage($"OnCustomPlayerDeath IsFactoryFight Respawn: {ex.StackTrace}");
                                Core.SendConsoleMessage($"OnCustomPlayerDeath IsFactoryFight Respawn: {ex.Message}");
                            }
                        }, 2500);

                        ServerAccountModel.SetPlayerEXP(killer.SocialClubId, ServerAccountModel.GetPlayerEXP(killer.SocialClubId) + 2);

                        if (ServerAccountModel.GetPlayerEXP(killer.SocialClubId) >= ServerAccountModel.GetPlayerLevel(killer.SocialClubId) * 25)
                        {
                            ServerAccountModel.SetPlayerEXP(killer.SocialClubId, 0);
                            ServerAccountModel.SetPlayerLevel(killer.SocialClubId, ServerAccountModel.GetPlayerLevel(killer.SocialClubId) + 1);
                        }
                    }
                    else if (accData.IsStreetFight)
                    {
                        if (player.HasData("DealerLoot"))
                        {
                            HideAndSeek.DealerLootDrop = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_cs_heist_bag_01"), new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 0.5), player.Rotation, 255, 0);
                            player.SetClothes(5, 0, 0);
                            player.ResetData("DealerLoot");
                            HideAndSeek.DealerEventPlayerBlip.Position = player.Position;
                            HideAndSeek.DealerBlipTimer.Kill();
                        }

                        ServerAccountModel.UpdatePlayerDeaths(player);
                        ServerAccountModel.UpdatePlayerKills(killer);
                        killer.setPlayerOnlyArmor(100);

                        NAPI.Task.Run(() =>
                        {
                            try
                            {
                                if (player == null || !player.Exists) return;
                                NAPI.Player.SpawnPlayer(player, ServerTeamModel.GetTeamSpawnPosition(player.getCurrentTeamId()), ServerTeamModel.GetTeamSpawnRotation(player.getCurrentTeamId()).Z);
                            }
                            catch (Exception ex)
                            {
                                Core.SendConsoleMessage($"OnCustomPlayerDeath StreetFight Respawn: {ex.StackTrace}");
                                Core.SendConsoleMessage($"OnCustomPlayerDeath StreetFight Respawn: {ex.Message}");
                            }
                        }, 2500);

                        ServerAccountModel.SetPlayerEXP(killer.SocialClubId, ServerAccountModel.GetPlayerEXP(killer.SocialClubId) + 2);

                        if (ServerAccountModel.GetPlayerEXP(killer.SocialClubId) >= ServerAccountModel.GetPlayerLevel(killer.SocialClubId) * 25)
                        {
                            ServerAccountModel.SetPlayerEXP(killer.SocialClubId, 0);
                            ServerAccountModel.SetPlayerLevel(killer.SocialClubId, ServerAccountModel.GetPlayerLevel(killer.SocialClubId) + 1);
                        }
                    }
                    else if (player.GetSharedData<bool>("IS_IN_OVO") && killer.GetSharedData<bool>("IS_IN_OVO"))
                    {
                        if (player.GetData<int>("OVO_ROUNDS") > 1 && killer.GetData<int>("OVO_ROUNDS") > 1)
                        {
                            int oldValP = player.GetData<int>("OVO_ROUNDS");
                            int oldValK = killer.GetData<int>("OVO_ROUNDS");

                            int oldCount = killer.GetData<int>("OVO_WIN_COUNT");
                            killer.SetData("OVO_WIN_COUNT", (oldCount+1));
                            
                            player.SetData("OVO_ROUNDS", (oldValP-1));
                            killer.SetData("OVO_ROUNDS", (oldValK-1));
                            
                            
                            ServerAccountModel.UpdatePlayerDeaths(player);
                            ServerAccountModel.UpdatePlayerKills(killer);
                            
                            NAPI.Task.Run(() =>
                            {
                                try
                                {
                                    if (player == null || !player.Exists) return;
                                    NAPI.Player.SpawnPlayer(player, OneVsOne.positionOne, OneVsOne.positionOneH);
                                    
                                    if (killer == null || !killer.Exists) return;
                                    NAPI.Player.SpawnPlayer(killer, OneVsOne.positionTwo, OneVsOne.positionTwoH);

                                    player.RemoveAllWeapons();
                                    player.givePlayerWeapons();
                                }
                                catch (Exception ex)
                                {
                                    Core.SendConsoleMessage(
                                        $"OnCustomPlayerDeath StreetFight Respawn: {ex.StackTrace}");
                                    Core.SendConsoleMessage($"OnCustomPlayerDeath StreetFight Respawn: {ex.Message}");
                                }
                            }, 2500);
                        }
                        else
                        {
                            player.ResetData("OVO_ROUNDS");
                            killer.ResetData("OVO_ROUNDS");

                            int oldCount = killer.GetData<int>("OVO_WIN_COUNT");
                            killer.SetData("OVO_WIN_COUNT", (oldCount+1));
                            
                            ServerAccountModel.UpdatePlayerDeaths(player);
                            ServerAccountModel.UpdatePlayerKills(killer);
                            NAPI.Entity.SetEntityDimension(player, 0);
                            NAPI.Entity.SetEntityDimension(killer, 0);

                            //player.SendChatMessage($"[~o~1vs1~w~] {killer.Name} hat das 1vs1 gewonnen.");

                            if (player.GetData<int>("OVO_WIN_COUNT") > killer.GetData<int>("OVO_WIN_COUNT"))
                            {
                                killer.sendLanguageToPlayer($"[~o~1vs1~w~] {player.Name} hat das 1vs1 gewonnen.",
                                    $"[~o~1vs1~w~] {player.Name} has won the 1vs1.",
                                    $"[~o~1vs1~w~] {player.Name} выиграл 1vs1.");

                                player.sendLanguageToPlayer($"[~o~1vs1~w~] Du hast das 1vs1 gegen {killer.Name} gewonnen.",
                                    $"[~o~1vs1~w~] You have won the 1vs1 against {killer.Name}.",
                                    $"[~o~1vs1~w~] Вы выиграли 1vs1 против {killer.Name}.");
                            }
                            else if (killer.GetData<int>("OVO_WIN_COUNT") > player.GetData<int>("OVO_WIN_COUNT"))
                            {
                                player.sendLanguageToPlayer($"[~o~1vs1~w~] {killer.Name} hat das 1vs1 gewonnen.",
                                    $"[~o~1vs1~w~] {killer.Name} has won the 1vs1.",
                                    $"[~o~1vs1~w~] {killer.Name} выиграл 1vs1.");

                                killer.sendLanguageToPlayer($"[~o~1vs1~w~] Du hast das 1vs1 gegen {player.Name} gewonnen.",
                                    $"[~o~1vs1~w~] You have won the 1vs1 against {player.Name}.",
                                    $"[~o~1vs1~w~] Вы выиграли 1vs1 против {player.Name}.");
                            }
                            else if (killer.GetData<int>("OVO_WIN_COUNT") == player.GetData<int>("OVO_WIN_COUNT"))
                            {
                                player.sendLanguageToPlayer($"[~o~1vs1~w~] Unentschieden. Niemand hat gewonnen.",
                                    $"[~o~1vs1~w~] Draw. Nobody has won.",
                                    $"[~o~1vs1~w~] Ничья. Никто не выиграл.");

                                killer.sendLanguageToPlayer($"[~o~1vs1~w~] Unentschieden. Niemand hat gewonnen.",
                                    $"[~o~1vs1~w~] Draw. Nobody has won.",
                                    $"[~o~1vs1~w~] Ничья. Никто не выиграл.");
                            }

                            //killer.SendChatMessage($"[~o~1vs1~w~] Du hast das 1vs1 gegen {player.Name} gewonnen.");

                            player.SetSharedData("IS_IN_OVO", false);
                            killer.SetSharedData("IS_IN_OVO", false);
                            
                            player.ResetData("OVO_WIN_COUNT");
                            killer.ResetData("OVO_WIN_COUNT");

                            NAPI.Task.Run(() =>
                            {
                                try
                                {
                                    if (player == null || !player.Exists) return;
                                    NAPI.Player.SpawnPlayer(player,
                                        ServerTeamModel.GetTeamSpawnPosition(player.getCurrentTeamId()),
                                        ServerTeamModel.GetTeamSpawnRotation(player.getCurrentTeamId()).Z);
                                    if (killer == null || !killer.Exists) return;
                                    NAPI.Player.SpawnPlayer(killer,
                                        ServerTeamModel.GetTeamSpawnPosition(killer.getCurrentTeamId()),
                                        ServerTeamModel.GetTeamSpawnRotation(killer.getCurrentTeamId()).Z);

                                    player.RemoveAllWeapons();
                                    player.givePlayerWeapons();
                                }
                                catch (Exception ex)
                                {
                                    Core.SendConsoleMessage(
                                        $"OnCustomPlayerDeath StreetFight Respawn: {ex.StackTrace}");
                                    Core.SendConsoleMessage($"OnCustomPlayerDeath StreetFight Respawn: {ex.Message}");
                                }
                            }, 2500);

                            accData.IsInOVO = false;
                            killerData.IsInOVO = false;

                            accData.IsStreetFight = true;
                            killerData.IsStreetFight = true;

                            player.RemoveAllWeapons();
                            player.givePlayerWeapons();
                        }

                        ServerAccountModel.SetPlayerEXP(killer.SocialClubId,
                            ServerAccountModel.GetPlayerEXP(killer.SocialClubId) + 2);

                        if (ServerAccountModel.GetPlayerEXP(killer.SocialClubId) >=
                            ServerAccountModel.GetPlayerLevel(killer.SocialClubId) * 25)
                        {
                            ServerAccountModel.SetPlayerEXP(killer.SocialClubId, 0);
                            ServerAccountModel.SetPlayerLevel(killer.SocialClubId,
                                ServerAccountModel.GetPlayerLevel(killer.SocialClubId) + 1);
                        }
                    }
                }
                else //kill == null || !killer.Exists
                {
                    if (accData.IsFFA)
                    {
                        ServerFFASpawnModel.GiveFFAWeapons(player, player.GetSharedData<int>("PLAYER_ARENA_ID"));
                        ServerAccountModel.UpdatePlayerDeaths(player);
                        NAPI.Task.Run(() =>
                        {
                            try
                            {
                                if (player == null || !player.Exists) return;
                                ServerFFASpawnModel.GetRandomSpawnPoint(player, player.GetSharedData<int>("PLAYER_ARENA_ID"));
                            }
                            catch (Exception ex)
                            {
                                Core.SendConsoleMessage($"OnCustomPlayerDeath StreetFight Respawn: {ex.StackTrace}");
                                Core.SendConsoleMessage($"OnCustomPlayerDeath StreetFight Respawn: {ex.Message}");
                            }
                        }, 2500);
                    }
                    else if (accData.IsFactoryFight)
                    {
                        ServerAccountModel.UpdatePlayerDeaths(player);

                        accData.IsFactoryFight = false;

                        NAPI.Task.Run(() =>
                        {
                            try
                            {
                                if (player == null || !player.Exists) return;
                                player.Dimension = 0;
                                NAPI.Entity.SetEntityDimension(player, 0);
                                NAPI.Player.SpawnPlayer(player, ServerTeamModel.GetTeamSpawnPosition(player.getCurrentTeamId()), ServerTeamModel.GetTeamSpawnRotation(player.getCurrentTeamId()).Z);
                            }
                            catch (Exception ex)
                            {
                                Core.SendConsoleMessage($"OnCustomPlayerDeath IsFactoryFight Respawn: {ex.StackTrace}");
                                Core.SendConsoleMessage($"OnCustomPlayerDeath IsFactoryFight Respawn: {ex.Message}");
                            }
                        }, 2500);
                    }
                    else if (accData.IsStreetFight)
                    {
                        if (player.HasData("DealerLoot"))
                        {
                            HideAndSeek.DealerLootDrop = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_cs_heist_bag_01"), new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 0.5), player.Rotation, 255, 0);
                            player.SetClothes(5, 0, 0);
                            player.ResetData("DealerLoot");
                            HideAndSeek.DealerEventPlayerBlip.Position = player.Position;
                            HideAndSeek.DealerBlipTimer.Kill();
                        }

                        ServerAccountModel.UpdatePlayerDeaths(player);
                        NAPI.Task.Run(() =>
                        {
                            try
                            {
                                if (player == null || !player.Exists) return;
                                NAPI.Player.SpawnPlayer(player, ServerTeamModel.GetTeamSpawnPosition(player.getCurrentTeamId()), ServerTeamModel.GetTeamSpawnRotation(player.getCurrentTeamId()).Z);
                            }
                            catch (Exception ex)
                            {
                                Core.SendConsoleMessage($"OnCustomPlayerDeath StreetFight Respawn: {ex.StackTrace}");
                                Core.SendConsoleMessage($"OnCustomPlayerDeath StreetFight Respawn: {ex.Message}");
                            }
                        }, 2500);
                    }
                    else if (accData.IsInOVO)
                    {
                        player.ResetSharedData("IS_IN_OVO");
                        ServerAccountModel.UpdatePlayerDeaths(player);
                        NAPI.Task.Run(() =>
                        {
                            try
                            {
                                if (player == null || !player.Exists) return;
                                ServerFFASpawnModel.GetRandomSpawnPoint(player, player.getCurrentTeamId());
                
                                player.RemoveAllWeapons();
                                player.givePlayerWeapons();
                            }
                            catch (Exception ex)
                            {
                                Core.SendConsoleMessage($"OnCustomPlayerDeath StreetFight Respawn: {ex.StackTrace}");
                                Core.SendConsoleMessage($"OnCustomPlayerDeath StreetFight Respawn: {ex.Message}");
                            }
                        }, 2500);
                    }
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnCustomPlayerDeath: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnCustomPlayerDeath: {exception.Message}");
            }
        }
    }
}
