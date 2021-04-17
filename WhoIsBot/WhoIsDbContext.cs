using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhoIsBot.Models;

namespace WhoIsBot
{
    public class WhoIsDbContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Tag> Tags { get; set; }

        /// <inheritdoc />
        protected WhoIsDbContext()
        {
        }

        /// <inheritdoc />
        public WhoIsDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}