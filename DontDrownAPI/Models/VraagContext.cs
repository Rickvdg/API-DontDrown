using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontDrownAPI.Models
{
    public class VraagContext : DbContext
    {
        public VraagContext(DbContextOptions<VraagContext> options) : base (options)
        {
        }

        public DbSet<VraagItem> VraagItems { get; set; }
    }
}
