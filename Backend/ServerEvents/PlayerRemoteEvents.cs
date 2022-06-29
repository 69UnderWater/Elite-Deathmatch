using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gangwar.DbModels.Models;
using Gangwar.Objects.MainMenuObjects;
using Gangwar.ServerModels;
using Gangwar.Systems.EventSystems;
using GTANetworkAPI;

namespace Gangwar.ServerEvents
{
    public class PlayerRemoteEvents : Script
    {
        [RemoteEvent("Server:Open:MainMenu")]
        public static void OnServerOpenMainMenu(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;

                    Accounts accounts =
                        ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                    if (accounts == null || accounts.FirstLogin) return;

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

                    if (accounts.IsStreetFight && player.HasData("DealerLoot"))
                    {
                        HideAndSeek.DealerLootDrop = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_cs_heist_bag_01"), new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 0.5), player.Rotation, 255, 0);
                        player.SetClothes(5, 0, 0);
                        player.ResetData("DealerLoot");
                        HideAndSeek.DealerEventPlayerBlip.Position = player.Position;
                        HideAndSeek.DealerBlipTimer.Kill();
                    }
                    
                    var playerData = new PlayerDataObject(accounts.Kills, accounts.Deaths, accounts.Money,
                        accounts.Level, accounts.PlayedHours, true, accounts.SelectedLanguage);
                    
                    player.TriggerEvent("client:createMainMenuBrowser", NAPI.Util.ToJson(playerData), NAPI.Util.ToJson(teamDataList),
                        NAPI.Util.ToJson(ffaDataList));
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnServerOpenMainMenu: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnServerOpenMainMenu: {exception.Message}");
            }
        }
    }
}