using System;
using System.Collections.Generic;
using GTANetworkAPI;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gangwar.DbModels.Models
{
    public partial class Dealer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int DealerId { get; set; }
        public string LocationName { get; set; }
        public Vector3 DealerPosition { get; set; }
        public Vector3 DealerRotation { get; set; }
        public Vector3 AbgabeDealerPosition { get; set; }
        public Vector3 AbgabeDealerRotation { get; set; }
    }
}
