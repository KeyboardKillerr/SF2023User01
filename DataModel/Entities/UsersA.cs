using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

#nullable disable

namespace DataModel.Entities
{
    public partial class UsersA
    {
        public int UserId { get; set; } 
        public DateTime? BirthDate { get; set; }
        public int? CountryId { get; set; }
        public string Phone { get; set; }
        public int? DirectionId { get; set; }
        public string Password { get; set; }
        public byte[] Photo { get; set; }

        public virtual Country Country { get; set; }
        public virtual Direction Direction { get; set; }

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
