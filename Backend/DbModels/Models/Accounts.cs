using System;
using Gangwar.Objects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gangwar.Objects.WeaponObject;

namespace Gangwar.DbModels.Models
{
    public partial class Accounts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Username { get; set; }
        public ulong SocialclubId { get; set; }
        public string SocialclubName { get; set; }
        public string HardwareId { get; set; }
        public int AdminLevel { get; set; }
        public int Level { get; set; }
        public int Prestige { get; set; }
        public int CurrentXP { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Money { get; set; }
        public int PlayedHours { get; set; }
        public int SelectedLanguage { get; set; }
        public int PrivateFrakId { get; set; }
        public int PrivateFrakRank { get; set; }
        public bool IsPrivateFrak { get; set; }
        public WeaponObject Weapons { get; set; }

        public bool IsBanned { get; set; }
        public bool IsMuted { get; set; }
        public bool IsTimeBanned { get; set; }
        public DateTime BanDate { get; set; }
        public DateTime TimeBanUntil { get; set; }
        public int Warns { get; set; }
        public string BanReason { get; set; }

        public string guildMemberId { get; set; }
        public string discordSyncCode { get; set; }
        public DailyMissionObject DailyMission { get; set; }

        [NotMapped]
        public bool IsAduty { get; set; } = false;
        
        [NotMapped]
        public bool IsVanish { get; set; } = false;

        [NotMapped]
        public bool IsSpectating { get; set; }

        [NotMapped]
        public bool IsFFA { get; set; } = false;

        [NotMapped]
        public bool IsStreetFight { get; set; } = false;

        [NotMapped]
        public bool IsFactoryFight { get; set; } = false;

        [NotMapped]
        public bool IsInOVO { get; set; } = false;

        [NotMapped]
        public int CurrentTeamId { get; set; } = 0;

        [NotMapped] public bool FirstLogin { get; set; } = false;
    }
}
