using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API__NET_5.Models.Dto
{
    public class response
    {
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        public  string Message { get; set; }

        public List<string> Error { get; set; }
    }
}
