using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontDrownAPI.Models
{
    public class AntwoordItem
    {
        public long Id { get; set; }
        public string Waarde { get; set; }
        public int Correctness { get; set; } // 1 meaning true, folowing items meaning false/less true
    }
}
