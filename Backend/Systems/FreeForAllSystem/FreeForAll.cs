using System;
using System.Collections.Generic;
using System.Linq;
using GTANetworkAPI;
using Gangwar.ServerModels;

namespace Gangwar.Systems.FreeForAllSystem
{
    public class FreeForAll : Script
    {
        [RemoteEvent("Server:MainMenu:SelectFfaArena")]
        public static void OnServerFFAJoin(Player player, int id)
        {
            try
            {
                if (player == null || !player.Exists) return;

                var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;
                
                var arenaData = ServerFFASpawnModel.FFASpawns_.FirstOrDefault(x => x.ArenaId == id); 
                if (arenaData == null) return;

                int currentPlayers = arenaData.OnlinePlayers.Count;
                if (arenaData.maxPlayers <= currentPlayers) return;

                player.setInvisible(false);
                player.setFreezed(false);

                player.RemoveAllWeapons();
                
                player.Dimension = (uint) arenaData.ArenaId * 81;
                
                ServerFFASpawnModel.GetRandomSpawnPoint(player, id);
                ServerFFASpawnModel.GiveFFAWeapons(player, id);
                
                player.SetSharedData("PLAYER_ARENA_ID", id);

                accData.IsFFA = true;
                accData.IsStreetFight = false;
                accData.IsInOVO = false;
                accData.IsFactoryFight = false;

                accData.FirstLogin = false;
                player.SetSharedData("IS_IN_FFA", true);

                player.TriggerEvent("client:deleteMainMenuBrowser");
                
                player.TriggerEvent("client:createHudBrowser");
                player.TriggerEvent("client:updateHud", accData.Kills, accData.Deaths, accData.Level);

                foreach (var oldarenadata in ServerFFASpawnModel.FFASpawns_.FindAll(x => x.OnlinePlayers.Contains(player.SocialClubId)))
                {
                    oldarenadata.OnlinePlayers.Remove(player.SocialClubId);
                }

                foreach (var oldteamdata in ServerTeamModel.Teams_.FindAll(x => x.OnlineTeamPlayers.Contains(player.SocialClubId)))
                {
                    oldteamdata.OnlineTeamPlayers.Remove(player.SocialClubId);
                }
                
                arenaData.OnlinePlayers.Add(player.SocialClubId);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnServerFFAJoin: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnServerFFAJoin: {exception.Message}");
            }
        }
    }
}
