using System;
using GTANetworkAPI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gangwar.Objects;

namespace Gangwar.DbModels.Models
{
    public partial class FFASpawns
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ArenaId { get; set; }
        public string ArenaName { get; set; }
        public int maxPlayers { get; set; }
        public List<FreeForAllObject> Spawns { get; set; }
        public FfaWeaponObject Weapons { get; set; }
        
        [NotMapped]
        public List<ulong> OnlinePlayers = new List<ulong>();
    }
}
