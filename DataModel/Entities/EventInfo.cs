using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Entities
{
    public partial class EventInfo
    {
        public int EventInfoId { get; set; }
        public int? EventId { get; set; }
        public string Password { get; set; }
        public byte[] Photo { get; set; }

        public virtual Event Event { get; set; }
    }
}
