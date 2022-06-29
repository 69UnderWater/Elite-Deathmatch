using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GTANetworkAPI;
using Gangwar.DbModels;
using Gangwar.DbModels.Models;

namespace Gangwar.ServerModels
{
    public class ServerDeathmatchModel : Script
    {
        public static List<Deathmatch> Deathmatch_ = new List<Deathmatch>();

        public static void SetDeathmatchTeams(int deathmatchId)
        {
            var deathmatchData = Deathmatch_.FirstOrDefault(x => x.DeathmatchId == deathmatchId);
            if (deathmatchData == null) return;

            foreach (var playerSocial in deathmatchData.Players)
            {
                var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == playerSocial);
                if (accData == null) return;

                Player player = NAPI.Player.GetPlayerFromName(accData.Username);
                if (player == null) return;

                deathmatchData.TeamOneName = ServerTeamModel.GetTeamNameById(accData.CurrentTeamId);
            }
        }

        public static void LoadDeathmatch(int deathmatchId)
        {
            var deathmatchData = Deathmatch_.FirstOrDefault(x => x.DeathmatchId == deathmatchId);
            if (deathmatchData == null) return;

            foreach (var playerSocial in deathmatchData.Players)
            {
                var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == playerSocial);
                if (accData == null) return;

                Player player = NAPI.Player.GetPlayerFromName(accData.Username);
                if (player == null) return;

                player.TriggerEvent("client:deathmatch:setup", deathmatchData.MaxDeaths, deathmatchData.TeamOneName, deathmatchData.TeamTwoName, deathmatchData.MaxPlayTime);
            }
        }
    }
}
