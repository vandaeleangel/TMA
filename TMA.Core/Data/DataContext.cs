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
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Chore> Chores { get; set; }
        public DbSet<TimeBlock> TimeBlocks { get; set; }

    }
}
