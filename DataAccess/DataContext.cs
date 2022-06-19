using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<DbFileModel> Files { get; set; }
        private readonly ILogger<DataContext> _logger;

        

        public DataContext(DbContextOptions options, ILogger<DataContext> logger)
            : base(options)
        {
            _logger = logger;
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }




    }
}