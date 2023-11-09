using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Entities
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            UsersAs = new HashSet<UsersA>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<UsersA> UsersAs { get; set; }
    }
}
