using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Gangwar.ServerModels;
using System.Linq;

namespace Gangwar.Systems.OneVsOneSystem
{
    public class OneVsOne : Script
    {
        public static Vector3 positionOne = new Vector3(79.91443, -863.4814, 134.77030);
        public static float positionOneH = -109.78549f;
        public static Vector3 positionTwo = new Vector3(124.62715, -880.54333, 134.77030);
        public static float positionTwoH = 69.91844f;

        public static void StartOneVsOne(Player sender, Player receiver)
        {
            if (sender == null || !sender.Exists || receiver == null || !receiver.Exists) return;

            var senderData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == sender.SocialClubId);
            if (senderData == null) return;

            var receiverData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == receiver.SocialClubId);
            if (receiverData == null) return;

            senderData.IsInOVO = true;
            receiverData.IsInOVO = true;

            senderData.IsStreetFight = false;
            senderData.IsInOVO = false;
            senderData.IsFactoryFight = false;

            receiverData.IsStreetFight = false;
            receiverData.IsInOVO = false;
            receiverData.IsFactoryFight = false;

            sender.Position = positionOne;
            sender.Heading = positionOneH;

            receiver.Position = positionTwo;
            receiver.Heading = positionTwoH;

            sender.SetSharedData("IS_IN_OVO", true);
            receiver.SetSharedData("IS_IN_OVO", true);
        }
    }
}
