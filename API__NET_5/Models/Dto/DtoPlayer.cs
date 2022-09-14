using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API__NET_5.Models.Dto
{
    public class DtoPlayer
    {
        
        public int id { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string equipo { get; set; }

        public int numero { get; set; }
    }
}
