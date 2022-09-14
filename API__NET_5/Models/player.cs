using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API__NET_5.Models
{
    public class player
    {
        [Key]
        public int id { get; set; }
        
        [Required]
        public string nombre { get; set; }

        [Required]
        public string apellido { get; set; }

        [Required]
        public string equipo { get; set; }

        [Required]
        public int numero { get; set; }

    }
}
