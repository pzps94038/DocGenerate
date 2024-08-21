using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerate.DatabaseContext
{
    public class DocGenerateDbContext : DbContext
    {
        public DbSet<DatabaseSetting> DatabaseSetting { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DocGenerateDb.db");
        }
    }
}
