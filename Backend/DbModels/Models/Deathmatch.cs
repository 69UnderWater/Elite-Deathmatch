using System;
using Gangwar.Objects;
using System.Collections.Generic;
using GTANetworkAPI;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gangwar.DbModels.Models
{
    public partial class Deathmatch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int DeathmatchId { get; set; }
        public DeathmatchTeamObject TeamOneSpawnPosition { get; set; }
        public DeathmatchTeamObject TeamTwoSpawnPosition { get; set; }
        public int MaxPlayTime { get; set; }
        public int MaxDeaths { get; set; }

        [NotMapped]
        public string TeamOneName { get; set; }

        [NotMapped]
        public string TeamTwoName { get; set; }

        [NotMapped]
        public List<ulong> Players = new List<ulong>();
    }
}
