using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontDrownAPI.Models
{
    public class Account
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public long RolId { get; set; }
        public string Rol { get; set; }
        public long SaveId { get; set; }
        public string SaveJson { get; set; }
        public string Classname { get; set; }
    }
}
