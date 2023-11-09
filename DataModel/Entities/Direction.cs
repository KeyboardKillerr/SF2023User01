using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Entities
{
    public partial class Direction
    {
        public Direction()
        {
            EventsDirections = new HashSet<EventsDirection>();
            UsersAs = new HashSet<UsersA>();
        }

        public int DirectionId { get; set; }
        public string DirectionName { get; set; }

        public virtual ICollection<EventsDirection> EventsDirections { get; set; }
        public virtual ICollection<UsersA> UsersAs { get; set; }
    }
}
