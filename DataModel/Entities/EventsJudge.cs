using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Entities
{
    public partial class EventsJudge
    {
        public int EventId { get; set; }
        public int JudgeId { get; set; }

        public virtual Event Event { get; set; }
        public virtual Judge Judge { get; set; }
    }
}
