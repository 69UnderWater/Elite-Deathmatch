using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gangwar.DbModels.Models
{
    public partial class DailyMissions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int DailyMissionId { get; set; }
        public string DailyMissionTitle { get; set; }
        public string DailyMissionContent { get; set; }
    }
}
