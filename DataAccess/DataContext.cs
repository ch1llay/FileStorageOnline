using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public sealed class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<DbFileInfo> InfoFiles { get; set; }
        public DbSet<DbFileData> DataFiles { get; set; }
        public DbSet<DbLink> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}