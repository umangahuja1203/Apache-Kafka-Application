using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealConsumer
{
    public  class PreciselyContext : DbContext
    {
        public DbSet<Precisely> Precisely { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=CHA-PLP41382;Database=ProgrammingDB;Trusted_Connection=True;");
        }

    }
}
