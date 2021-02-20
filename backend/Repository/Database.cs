using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Models;

namespace Repository
{
    public class Database : DbContext
    {
        public Database(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
            .UseLoggerFactory(LoggerFactory.Create(builder => { }));
    }
}
