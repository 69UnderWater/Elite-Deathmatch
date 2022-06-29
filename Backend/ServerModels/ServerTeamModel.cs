using System;
using System.Collections.Generic;
using System.Linq;
using Gangwar.DbModels.Models;
using Gangwar.DbModels;
using GTANetworkAPI;

namespace Gangwar.ServerModels
{
    public class ServerTeamModel : Script
    {
        public static List<Teams> Teams_ = new List<Teams>();

        public static void LoadTeams()
        {
            try
            {
                foreach (var teamData in Teams_.Where(x => x.IsPrivate == false))
                {
                    NAPI.Blip.CreateBlip(492, teamData.TeamSpawnPoint, 1f, (byte)teamData.BlipColor, teamData.TeamName, 255, 0, true, 0, 0);
                    NAPI.Ped.CreatePed(NAPI.Util.GetHashKey(teamData.TeamPedHash), teamData.TeamPedSpawnPoint, teamData.TeamPedSpawnRotation.Z, dimension: 0, invincible: true, frozen: true);
                }

                foreach (var teamData in Teams_.Where(x => x.IsPrivate == true))
                {
                    NAPI.Blip.CreateBlip(40, teamData.TeamSpawnPoint, 1f, (byte)teamData.BlipColor, teamData.TeamName, 255, 0, true, 0, 0);
                    NAPI.Ped.CreatePed(NAPI.Util.GetHashKey(teamData.TeamPedHash), teamData.TeamPedSpawnPoint, teamData.TeamPedSpawnRotation.Z, dimension: 0, invincible: true, frozen: true);
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"LoadTeams: {exception.StackTrace}");
                Core.SendConsoleMessage($"LoadTeams: {exception.Message}");
            }
        }

        public static int GetTeamIdByName(string teamName)
        {
            var teamData = Teams_.FirstOrDefault(x => x.TeamName == teamName);
            if (teamData == null) return -1;

            return teamData.TeamId;
        }

        public static string GetTeamNameById(int teamId)
        {
            var teamData = Teams_.FirstOrDefault(x => x.TeamId == teamId);
            if (teamData == null) return "undefined";

            return teamData.TeamName;
        }

        public static Color GetTeamPrimaryColorById(int teamId)
        {
            var teamData = Teams_.FirstOrDefault(x => x.TeamId == teamId);
            if (teamData == null) return new Color(0, 0, 0);

            return new Color(teamData.PrimaryColor.r, teamData.PrimaryColor.g, teamData.PrimaryColor.b);
        }

        public static Color GetTeamSecondaryColorById(int teamId)
        {
            var teamData = Teams_.FirstOrDefault(x => x.TeamId == teamId);
            if (teamData == null) return new Color(0, 0, 0);

            return new Color(teamData.SecondaryColor.r, teamData.SecondaryColor.g, teamData.SecondaryColor.b);
        }

        public static int GetTeamBlipColorById(int teamId)
        {
            var teamData = Teams_.FirstOrDefault(x => x.TeamId == teamId);
            if (teamData == null) return 0;

            return teamData.BlipColor;
        }

        public static bool IsTeamPrivate(int teamId)
        {
            var teamData = Teams_.FirstOrDefault(x => x.TeamId == teamId);
            if (teamData == null) return false;

            return teamData.IsPrivate;
        }

        public static Vector3 GetTeamSpawnPosition(int teamId)
        {
            var teamData = Teams_.FirstOrDefault(x => x.TeamId == teamId);
            if (teamData == null) return new Vector3(0, 0, 0);

            return teamData.TeamSpawnPoint;
        }

        public static Vector3 GetTeamSpawnRotation(int teamId)
        {
            var teamData = Teams_.FirstOrDefault(x => x.TeamId == teamId);
            if (teamData == null) return new Vector3(0, 0, 0);

            return teamData.TeamSpawnRotation;
        }
    }
}
