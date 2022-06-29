using System;
using System.Collections.Generic;
using System.Text;

namespace Gangwar.Objects
{
    public class DailyMissionObject
    {
        public DailyMissonContentObject DailyMissionOne { get; set; }
        public DailyMissonContentObject DailyMissionTwo { get; set; }
        public DailyMissonContentObject DailyMissionThree { get; set; }
        public DateTime LastDailyMissionDate { get; set; }

        public DailyMissionObject(DailyMissonContentObject DailyMissionOne, DailyMissonContentObject DailyMissionTwo, DailyMissonContentObject DailyMissionThree, DateTime LastDailyMissionDate)
        {
            this.DailyMissionOne = DailyMissionOne;
            this.DailyMissionTwo = DailyMissionTwo;
            this.DailyMissionThree = DailyMissionThree;
            this.LastDailyMissionDate = LastDailyMissionDate;
        }
    }


}
