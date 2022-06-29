using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gangwar.Objects;

namespace Gangwar.DbModels.Models
{
    public partial class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ulong SocialClubId { get; set; }
        public List<InventoryObject> Inventar { get; set; }
    }
}
