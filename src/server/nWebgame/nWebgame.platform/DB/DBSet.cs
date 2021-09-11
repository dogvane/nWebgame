using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using nWebgame.platform.Entitys;

namespace nWebgame.platform.DB {
    public class DBSet : Microsoft.EntityFrameworkCore.DbContext {
        string conString = "Database='{0}';Data Source={1};password={3};User ID={2};Port=3306;";

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            var con = String.Format (conString, "11", "192.168.1.62", "root", "");
            optionsBuilder.UseMySQL (con);
            base.OnConfiguring (optionsBuilder);
        }

        public DbSet<PlatformAccount> PlatformAccounts { get; set; }
    }
}