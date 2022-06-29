using Gangwar.Objects;
using Gangwar.ServerModels;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gangwar.DbModels.Models;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    class VehicleDelete : Script
    {
        [Command("clearcars")]
        public void cmd_clearcars (Player player)
        {
            Accounts accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
            if (accData == null) return;
            if (accData.AdminLevel < Convert.ToInt32(AdminRanksObject.STAFF)) return;

            //NAPI.Chat.SendChatMessageToAll("[~r~SERVER~w~] Es werden in 30 Sekunden alle ungenutzten Fahrzeuge gelöscht.");
            Core.sendLanguageToEveryone("[~r~SERVER~w~] Es werden in 30 Sekunden alle ungenutzten Fahrzeuge gelöscht.",
                "[~r~SERVER~w~] All unused vehicles will be deleted in 30 seconds.",
                "[~r~СЕРВЕР~w~] Все неиспользуемые автомобили будут удалены через 30 секунд.");

            new Timer(() =>
            {
                foreach (var vehicle in NAPI.Pools.GetAllVehicles().Where(x => x != null && x.Exists && NAPI.Pools.GetAllPlayers().FindAll(y => y.IsInVehicle && y.Vehicle == x).Count == 0 && !x.HasData("weapontruck")))
                {
                    vehicle.Delete();
                }
                //NAPI.Chat.SendChatMessageToAll("[~r~SERVER~w~] Es wurden alle ungenutzten Fahrzeuge gelöscht.");
                Core.sendLanguageToEveryone("[~r~SERVER~w~] Es wurden alle ungenutzten Fahrzeuge gelöscht.",
                    "[~r~SERVER~w~] All unused vehicles were deleted.",
                    "[~r~СЕРВЕР~w~] Все неиспользуемые автомобили были удалены.");
            }, 30000, "[VehicleDelete] Delay", 1);
        }
    }
}
