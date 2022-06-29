using System;
using System.Collections.Generic;
using System.Text;

namespace Gangwar.Objects
{
    public class DailyMissonContentObject
    {
        public int DailyMissionId { get; set; }
        public string DailyMissionTitle { get; set; }
        public string DailyMissionContent { get; set; }

        public DailyMissonContentObject(int DailyMissionId, string DailyMissionTitle, string DailyMissionContent)
        {
            this.DailyMissionId = DailyMissionId;
            this.DailyMissionTitle = DailyMissionTitle;
            this.DailyMissionContent = DailyMissionContent;
        }
    }
}
