using System;
using GTANetworkAPI;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gangwar.DbModels.Models
{
    public partial class TeamClothing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TeamId { get; set; }
        public int TeamClothingId { get; set; }
        public int Gender { get; set; }

        public int MaskDrawable { get; set; }
        public int MaskTexture { get; set; }
        public int TorsoDrawable { get; set; }
        public int TorsoTexture { get; set; }
        public int LegsDrawable { get; set; }
        public int LegsTexture { get; set; }
        public int BagsNParachuteDrawable { get; set; }
        public int BagsNParachuteTexture { get; set; }
        public int ShoeDrawable { get; set; }
        public int ShoeTexture { get; set; }
        public int AccessiorDrawable { get; set; }
        public int AccessiorTexture { get; set; }
        public int UndershirtDrawable { get; set; }
        public int UndershirtTexture { get; set; }
        public int BodyArmorDrawable { get; set; }
        public int BodyArmorTexture { get; set; }
        public int TopDrawable { get; set; }
        public int TopTexture { get; set; }

        public int HatsDrawable { get; set; }
        public int HatsTexture { get; set; }
        public int GlassesDrawable { get; set; }
        public int GlassesTexture { get; set; }
    }
}
