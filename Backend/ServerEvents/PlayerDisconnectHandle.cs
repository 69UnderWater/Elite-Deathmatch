using Gangwar.Systems.EventSystems;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using Gangwar.ServerModels;

namespace Gangwar.ServerEvents
{
    public class PlayerDisconnectHandle : Script
    {
		[ServerEvent(Event.PlayerDisconnected)]
		public void OnPlayerDisconnect(Player player, DisconnectionType type, string reason)
		{
            try
            {
                if (player == null || !player.Exists) return;

                foreach (var oldteamdata in ServerTeamModel.Teams_.FindAll(x => x.OnlineTeamPlayers.Contains(player.SocialClubId)))
                {
                    oldteamdata.OnlineTeamPlayers.Remove(player.SocialClubId);
                }

                if (player.HasData("DealerLoot"))
                {
                    HideAndSeek.DealerLootDrop = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_cs_heist_bag_01"), new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 0.5), player.Rotation, 255, 0);
                    player.SetClothes(5, 0, 0);
                    player.ResetData("DealerLoot");
                    HideAndSeek.DealerEventPlayerBlip.Position = player.Position;
                    HideAndSeek.DealerBlipTimer.Kill();
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage("OnPlayerDisconnect: " + exception.Message);
                Core.SendConsoleMessage("OnPlayerDisconnect: " + exception.StackTrace);
            }
		}
	}
}
