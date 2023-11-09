using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Entities
{
    public partial class EventsDirection
    {
        public int EventId { get; set; }
        public int DirectionId { get; set; }

        public virtual Direction Direction { get; set; }
        public virtual Event Event { get; set; }
    }
}
