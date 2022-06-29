using System;
using System.Collections.Generic;
using GTANetworkAPI;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gangwar.Objects.TeamObject;

namespace Gangwar.DbModels.Models
{
    public partial class Teams
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TeamId { get; set; }
        public string TeamName {  get; set; }
        public string ShortName { get; set; }
        public Vector3 TeamSpawnPoint { get; set; }
        public Vector3 TeamSpawnRotation { get; set; }
        public int BlipColor { get; set; }
        public ColorObjects PrimaryColor { get; set; }
        public ColorObjects SecondaryColor { get; set; }
        public bool IsPrivate { get; set; }
        public string TeamPedHash { get; set; }
        public Vector3 TeamPedSpawnPoint { get; set; }
        public Vector3 TeamPedSpawnRotation { get; set; }

        [NotMapped]
        public List<ulong> OnlineTeamPlayers = new List<ulong>();
    }
}
