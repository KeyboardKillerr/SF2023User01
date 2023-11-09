using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

#nullable disable

namespace DataModel.Entities
{
    public partial class Moderator
    {
        public Moderator()
        {
            Events = new HashSet<Event>();
        }

        public int ModeratorId { get; set; }
        public string ModeratorName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public static string ToHashString(string pass)
        {
            if (string.IsNullOrWhiteSpace(pass)) return "";
            using SHA1 hash = SHA1.Create();
            return string
                .Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(pass))
                .Select(x => x.ToString()));
        }
    }
}
