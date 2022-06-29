using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gangwar.Objects;

namespace Gangwar.DbModels.Models
{
    public partial class AdminRanks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Permission { get; set; }
        public string ChatColor { get; set; }
        public AdminClothingObject AdminClothing { get; set; }
    }
}
