using System;
using Gangwar.Objects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GTANetworkAPI;

namespace Gangwar.DbModels.Models
{
    public partial class WeaponTruck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public WeaponTruckPositionObject SpawnPosition { get; set; }
        public Vector3 ReturnPosition { get; set; }
    }
}
