using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DontDrownAPI.Models;

namespace dontdrown_ef.Models
{
    public class VraagItem
    {
        public long Id { get; set; }
        [Required]
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