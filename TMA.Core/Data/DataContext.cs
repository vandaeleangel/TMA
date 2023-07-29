using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMA.Core.Entities;

namespace TMA.Core.Data
{
    public class DataContext :DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Chore> Chores { get; set; }
        public DbSet<TimeBlock> TimeBlocks { get; set; }

        public DataContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TMAdb;Trusted_Connection=True;TrustServerCertificate=true;");
            optionsBuilder.EnableSensitiveDataLogging(true);
        }




    }
}
