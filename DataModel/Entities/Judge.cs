using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

#nullable disable

namespace DataModel.Entities
{
    public partial class Judge
    {
        public Judge()
        {
            EventsJudges = new HashSet<EventsJudge>();
        }

        public int JudgeId { get; set; }
        public string JudgeName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<EventsJudge> EventsJudges { get; set; }

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
