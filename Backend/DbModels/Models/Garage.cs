using System;
using Gangwar.Objects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GTANetworkAPI;

namespace Gangwar.DbModels.Models
{
    public partial class Garage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int TeamId { get; set; }
        public Vector3 GaragePosition { get; set; }
        public Vector3 GarageOutParkPoint { get; set; }
        public List<ParkoutPointObject> ParkoutPoints { get; set; }
        public List<GarageVehicleObject> Vehicles { get; set; }
    }
}
