using System;
using System.Collections.Generic;
using System.Linq;
using GTANetworkAPI;
using Gangwar.DbModels.Models;

namespace Gangwar.ServerModels
{
    public class ServerDailyMissionModel : Script
    {
        public static List<DailyMissions> DailyMissions_ = new List<DailyMissions>();

        public static void GetRandomDailyMissions(Player player)
        {
            var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
            if (accData == null) return;

            int randomOne = new Random(1).Next(10);
            int randomTwo = new Random(11).Next(20);
            int randomThree = new Random(21).Next(30);

            var dailyDataOne = DailyMissions_.FirstOrDefault(x => x.DailyMissionId == randomOne);
            if (dailyDataOne == null) return;

            var dailyDataTwo = DailyMissions_.FirstOrDefault(x => x.DailyMissionId == randomTwo);
            if (dailyDataTwo == null) return;

            var dailyDataThree = DailyMissions_.FirstOrDefault(x => x.DailyMissionId == randomThree);
            if (dailyDataThree == null) return;

            accData.DailyMission.DailyMissionOne.DailyMissionId = dailyDataOne.DailyMissionId;
            accData.DailyMission.DailyMissionOne.DailyMissionTitle = dailyDataOne.DailyMissionTitle;
            accData.DailyMission.DailyMissionOne.DailyMissionContent = dailyDataOne.DailyMissionContent;

            accData.DailyMission.DailyMissionTwo.DailyMissionId = dailyDataTwo.DailyMissionId;
            accData.DailyMission.DailyMissionTwo.DailyMissionTitle = dailyDataTwo.DailyMissionTitle;
            accData.DailyMission.DailyMissionTwo.DailyMissionContent = dailyDataTwo.DailyMissionContent;

            accData.DailyMission.DailyMissionThree.DailyMissionId = dailyDataThree.DailyMissionId;
            accData.DailyMission.DailyMissionThree.DailyMissionTitle = dailyDataThree.DailyMissionTitle;
            accData.DailyMission.DailyMissionThree.DailyMissionContent = dailyDataThree.DailyMissionContent;
        }

        public static bool IsNewDailyMission(ulong socialClubId)
        {
            var accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == socialClubId);
            if (accData == null) return false;

            return (accData.DailyMission.LastDailyMissionDate < DateTime.Now);
        }
    }
}
