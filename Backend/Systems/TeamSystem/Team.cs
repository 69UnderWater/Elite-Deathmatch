using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Gangwar.ServerModels;
using System.Linq;
using System.Text.Unicode;
using Gangwar.DbModels.Models;
using Gangwar.Objects.MainMenuObjects;
using GTANetworkMethods;
using Player = GTANetworkAPI.Player;

namespace Gangwar.Systems.TeamSystem
{
    public class Team : Script
    {
        [RemoteEvent("Server:MainMenu:RequestCurrentTeam")]
        public static void OnServerMainMenuRequestCurrentTeam(Player player, int teamId)
        {
            try
            {
                if (player == null || !player.Exists) return;

                var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;

                var teamData = ServerTeamModel.Teams_.FirstOrDefault(x => x.TeamId == teamId);
                if (teamData == null) return;

                if (!teamData.IsPrivate)
                {
                    accData.CurrentTeamId = teamId;
                    accData.FirstLogin = false;

                    player.Dimension = (uint) (NAPI.Pools.GetAllPlayers().Count + accData.Id);
                    player.Position = new Vector3(-1539.6097, -574.4865, 25.707903);
                    player.Rotation = new Vector3(0, 0, -146.42467);

                    List<TeamClothing> teamClothing = new List<TeamClothing>();

                    foreach (var teams in ServerTeamClothingModel.TeamClothing_.Where(x => x.TeamId == teamId))
                    {
                        teamClothing.Add(teams);
                    }

                    player.SetSharedData("PLAYER_TEAM_ID", teamData.TeamId);
                    
                    // Show Outfit Selection
                    player.TriggerEvent("client:createTeamClothingBrowser", NAPI.Util.ToJson(teamClothing));
                }
                else
                {
                    if (accData.IsPrivateFrak && accData.PrivateFrakId == teamData.TeamId)
                    {
                        accData.CurrentTeamId = teamId;
                        accData.FirstLogin = false;

                        player.Dimension = (uint) (NAPI.Pools.GetAllPlayers().Count + accData.Id);
                        player.Position = new Vector3(-1539.6097, -574.4865, 25.707903);
                        player.Rotation = new Vector3(0, 0, -146.42467);

                        List<TeamClothing> teamClothing = new List<TeamClothing>();

                        foreach (var teams in ServerTeamClothingModel.TeamClothing_.Where(x => x.TeamId == teamId))
                        {
                            teamClothing.Add(teams);
                        }

                        player.SetSharedData("PLAYER_TEAM_ID", teamData.TeamId);
                    
                        // Show Outfit Selection
                        player.TriggerEvent("client:createTeamClothingBrowser", NAPI.Util.ToJson(teamClothing));
                    }
                    else
                    {

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

                        var playerData = new PlayerDataObject(accData.Kills, accData.Deaths, accData.Money,
                            accData.Level, accData.PlayedHours, true, accData.SelectedLanguage);
                        
                        player.TriggerEvent("client:createMainMenuBrowser", NAPI.Util.ToJson(playerData),
                            NAPI.Util.ToJson(teamDataList), NAPI.Util.ToJson(ffaDataList));
                    }
                }

                foreach (var oldteamData in ServerTeamModel.Teams_.FindAll(x => x.OnlineTeamPlayers.Contains(player.SocialClubId)))
                {
                    oldteamData.OnlineTeamPlayers.Remove(player.SocialClubId);
                }

                foreach (var oldarenadata in ServerFFASpawnModel.FFASpawns_.FindAll(x => x.OnlinePlayers.Contains(player.SocialClubId)))
                {
                    oldarenadata.OnlinePlayers.Remove(player.SocialClubId);
                }
                
                teamData.OnlineTeamPlayers.Add(player.SocialClubId);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnServerMainMenuRequestTeams: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnServerMainMenuRequestTeams: {exception.Message}");
            }
        }

        [RemoteEvent("Server:TeamClothing:SelectTeamClothing")]
        public static void OnServerTeamClothingSelectCurrentClothing(Player player, int clothingId)
        {
            try
            {
                if (player == null || !player.Exists || string.IsNullOrEmpty(clothingId.ToString())) return;

                var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;

                var teamData = ServerTeamModel.Teams_.FirstOrDefault(x => x.TeamId == player.getCurrentTeamId());
                if (teamData == null) return;

                ServerTeamClothingModel.LoadTeamClothing(player, clothingId);
                
                if (player.HasSharedData("IS_IN_FFA"))
                    player.SetSharedData("IS_IN_FFA", false);
                
                accData.IsStreetFight = true;
                accData.IsInOVO = false;
                accData.IsFactoryFight = false;
                accData.IsFFA = false;

                player.Position = teamData.TeamSpawnPoint;
                player.Rotation = teamData.TeamSpawnRotation;
                
                player.setPlayerHealth(100, 100);

                player.setInvisible(false);
                player.setFreezed(false);
                player.Dimension = 0;
                
                player.RemoveAllWeapons();
                player.givePlayerWeapons();
                
                // Show Hud and Initialize it
                player.TriggerEvent("client:deleteTeamClothingBrowser");
                
                player.TriggerEvent("client:createHudBrowser");
                player.TriggerEvent("client:updateHud", accData.Kills, accData.Deaths, accData.Level);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnServerTeamClothingSelectCurrentClothing: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnServerTeamClothingSelectCurrentClothing: {exception.Message}");
            }
        }
    }
}
