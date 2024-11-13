using Microsoft.EntityFrameworkCore;
using StreamServer.Database.Models;

namespace StreamServer.Database
{
    public class StreamDbContext : DbContext
    {
        public StreamDbContext(DbContextOptions<StreamDbContext> options)
            : base(options)
        {
        }

        public DbSet<StreamInformation> StreamInformations { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
