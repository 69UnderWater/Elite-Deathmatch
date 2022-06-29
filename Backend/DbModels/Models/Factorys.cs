using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gangwar.DbModels.Models
{
    public partial class Factorys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public Vector3 FactoryPosition { get; set; }
        public Vector3 FactoryRotation { get; set; }
        public Vector3 FactoryRobPosition { get; set; }
    }
}
