using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<DbFileInfo> InfoFiles { get; set; }
        public DbSet<DbFileData> DataFiles { get; set; }
        public DbSet<DbOneTimeLinkModel> Links { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}