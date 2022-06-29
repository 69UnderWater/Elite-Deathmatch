using System;
using Gangwar.Objects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gangwar.DbModels.Models
{
    public partial class Items
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int PlayerId { get; set; }
        
        //public InventoryObject Inventory { get; set; }
    }
}
