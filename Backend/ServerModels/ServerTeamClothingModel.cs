using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTANetworkAPI;
using Gangwar.DbModels.Models;
using Gangwar.DbModels;

namespace Gangwar.ServerModels
{
    public class ServerTeamClothingModel : Script
    {
        private static List<string> gender = new List<string>()
        {
            "mp_m_freemode_01",
            "mp_f_freemode_01"
        };
        public static List<TeamClothing> TeamClothing_ = new List<TeamClothing>();

        public static void LoadTeamClothing(Player player, int clothingId)
        {
            try
            {
                if (player == null || !player.Exists) return;

                var teamClothingData = TeamClothing_.FirstOrDefault(x =>
                    x.TeamClothingId == clothingId && x.TeamId == player.getCurrentTeamId());
                if (teamClothingData == null) return;

                bool b = teamClothingData.Gender == 0;
                
                player.SetCustomization(b, new HeadBlend(), (byte)0, (byte)0, (byte)0, new float[0], new Dictionary<int, HeadOverlay>(), new Decoration[0]);

                player.SetClothes(1, teamClothingData.MaskDrawable, teamClothingData.MaskTexture);
                player.SetClothes(3, teamClothingData.TorsoDrawable, teamClothingData.TorsoTexture);
                player.SetClothes(4, teamClothingData.LegsDrawable, teamClothingData.LegsTexture);
                player.SetClothes(5, teamClothingData.BagsNParachuteDrawable, teamClothingData.BagsNParachuteTexture);
                player.SetClothes(6, teamClothingData.ShoeDrawable, teamClothingData.ShoeTexture);
                player.SetClothes(7, teamClothingData.AccessiorDrawable, teamClothingData.AccessiorTexture);
                player.SetClothes(8, teamClothingData.UndershirtDrawable, teamClothingData.UndershirtTexture);
                player.SetClothes(9, teamClothingData.BodyArmorDrawable, teamClothingData.BodyArmorTexture);
                player.SetClothes(11, teamClothingData.TopDrawable, teamClothingData.TopTexture);

                player.SetAccessories(0, teamClothingData.HatsDrawable, teamClothingData.HatsTexture);
                player.SetAccessories(1, teamClothingData.GlassesDrawable, teamClothingData.GlassesTexture);
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"LoadTeamClothing: {exception.StackTrace}");
                Core.SendConsoleMessage($"LoadTeamClothing: {exception.Message}");
            }
        }
    }
}
