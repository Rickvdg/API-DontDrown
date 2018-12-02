using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontDrownAPI.Models
{
    public class VraagItem
    {
        public long Id { get; set; }
        public string Vraag { get; set; }
        public VraagType Type { get; set; }
        public List<AntwoordItem> Antwoorden { get; set; }
        public string Hint { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        public bool Active { get; set; }

        public enum VraagType
        {
            Meerkeuze, Woord, Getal, Volgorde
        }
    }
}
