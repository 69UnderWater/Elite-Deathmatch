using System;
using System.Collections.Generic;
using System.Linq;
using Gangwar.DbModels.Models;
using Gangwar.ServerModels;
using GTANetworkAPI;
using Gangwar.Objects.MainMenuObjects;

namespace Gangwar.ServerEvents
{
    public class PlayerConnectionHandle : Script
    {
        [ServerEvent(Event.PlayerConnected)]
        public static async void OnPlayerConnected(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;
                
                player.SetSharedData("IS_IN_FFA", false);
                player.SetSharedData("IS_IN_OVO", false);
                player.SetSharedData("PLAYER_IS_ADUTY", false);
                player.SetSharedData("PLAYER_TEAM_ID", 1);

                List<TeamDataObject> teamDataList = new List<TeamDataObject>();

                foreach (var teams in ServerTeamModel.Teams_)
                {
                    teamDataList.Add(new TeamDataObject(teams.Id, teams.TeamName, teams.ShortName,
                        teams.OnlineTeamPlayers.Count, teams.IsPrivate));
                }
                    
                List<FfaDataObject> ffaDataList = new List<FfaDataObject>();

                foreach (var ffaSpawns in ServerFFASpawnModel.FFASpawns_)
                {
                    ffaDataList.Add(new FfaDataObject(ffaSpawns.Id, ffaSpawns.ArenaName,
                        ffaSpawns.OnlinePlayers.Count, ffaSpawns.maxPlayers));
                }
                
                ServerTeamClothingModel.LoadTeamClothing(player, 2);

                // player.TriggerEvent("toggleAntiCheatDetection");
                player.Position = new Vector3(-150.40808, -966.467, 259.1329);
                player.Dimension = (uint)(NAPI.Pools.GetAllPlayers().Count * 2);

                Accounts accounts =
                    ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accounts == null)
                {
                    Core.StartPlayedHoursTimer(player);
                    
                    var playerData = new PlayerDataObject(0, 0, 0, 0, 0, false, 1);
                    player.TriggerEvent("client:createMainMenuBrowser", NAPI.Util.ToJson(playerData), NAPI.Util.ToJson(teamDataList), NAPI.Util.ToJson(ffaDataList));
                }
                else
                {
                    
                    var playerData = new PlayerDataObject(accounts.Kills, accounts.Deaths, accounts.Money,
                        accounts.Level, accounts.PlayedHours, true, accounts.SelectedLanguage);

                    if (!ServerAccountModel.CheckHardwareId(player.Serial) &&
                        !ServerAccountModel.CheckSocialclubId(player.SocialClubId))
                    {
                        Core.StartPlayedHoursTimer(player);
                        
                        player.TriggerEvent("client:createMainMenuBrowser", NAPI.Util.ToJson(playerData),
                            NAPI.Util.ToJson(teamDataList), NAPI.Util.ToJson(ffaDataList));
                    }
                    else
                    {
                        if (!ServerAccountModel.IsPlayerBanned(player.SocialClubId))
                        {
                            await ServerAccountModel.LoadAccount(player);
                            player.TriggerEvent("client:createMainMenuBrowser", NAPI.Util.ToJson(playerData),
                                NAPI.Util.ToJson(teamDataList), NAPI.Util.ToJson(ffaDataList));
                        }
                        else
                        {
                            player.sendLanguageToPlayer("Du bist gebannt!", "You're banned from this Server!",
                                "Вы заблокированы на этом сервере!");

                            NAPI.Task.Run(() =>
                            {
                                if (player == null || !player.Exists) return;
                                
                                player.Kick();
                            }, 2500);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnPlayerConnted: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnPlayerConnted: {exception.Message}");
            }
        }
    }
}
