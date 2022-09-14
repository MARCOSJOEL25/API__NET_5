using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API__NET_5.Models
{
    public class user
    {
        [Key]
        public int id { get; set; }

        public string userName { get; set; }
        public byte[] passwordHash { get; set; }

        public byte[] passwordSalt { get; set; }
    }
}
