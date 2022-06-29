using System;
using System.Collections.Generic;
using System.Linq;
using Gangwar.DbModels.Models;
using GTANetworkAPI;
using Gangwar.Objects;
using Gangwar.ServerModels;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    public class GetposCommand : Script
    {
        [Command("getpos")]
        public static void GetposCMD(Player player)
        {
            try
            {
                if (player == null || !player.Exists) return;
                Accounts accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;

                if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.DEVELOPER)) return;

                Core.SendConsoleMessage($"");
                Core.SendConsoleMessage($"new Vector3({player.Position.X.ToString().Replace(",", ".")}, {player.Position.Y.ToString().Replace(",", ".")}, {player.Position.Z.ToString().Replace(",", ".")})");
                Core.SendConsoleMessage($"new Vector3({player.Rotation.X.ToString().Replace(",", ".")}, {player.Rotation.Y.ToString().Replace(",", ".")}, {player.Rotation.Z.ToString().Replace(",", ".")})");
                Core.SendConsoleMessage($"JSON: {NAPI.Util.ToJson(player.Position)}");
                Core.SendConsoleMessage($"JSON: {NAPI.Util.ToJson(player.Rotation)}");
                Core.SendConsoleMessage($"");
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"{exception.StackTrace}");
                Core.SendConsoleMessage($"{exception.Message}");
            }
        }
    }
}
