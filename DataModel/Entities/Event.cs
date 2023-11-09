using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Entities
{
    public partial class Event
    {
        public Event()
        {
            EventInfos = new HashSet<EventInfo>();
            EventsDirections = new HashSet<EventsDirection>();
            EventsJudges = new HashSet<EventsJudge>();
        }

        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime? StartDate { get; set; }
        public int? Days { get; set; }
        public string Activity { get; set; }
        public int? Day { get; set; }
        public TimeSpan? StartTime { get; set; }
        public int? ModeratorId { get; set; }
        public string Winner { get; set; }

        public virtual Moderator Moderator { get; set; }
        public virtual ICollection<EventInfo> EventInfos { get; set; }
        public virtual ICollection<EventsDirection> EventsDirections { get; set; }
        public virtual ICollection<EventsJudge> EventsJudges { get; set; }
    }
}
