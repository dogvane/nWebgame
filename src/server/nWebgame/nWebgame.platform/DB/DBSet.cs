using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using nWebgame.platform.Entitys;

namespace nWebgame.platform.DB {
    public class DBSet : Microsoft.EntityFrameworkCore.DbContext {
        string conString = "Database='{0}';Data Source={1};password={3};User ID={2};Port=3306;";

        [Obsolete]
        public static readonly LoggerFactory loggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            var con = String.Format (conString, "11", "192.168.1.62", "root", "");
            optionsBuilder.UseMySQL (con);
            //optionsBuilder.UseLoggerFactory(loggerFactory);

            base.OnConfiguring (optionsBuilder);
        }

        public DbSet<PlatformAccount> PlatformAccounts { get; set; }
    }
}