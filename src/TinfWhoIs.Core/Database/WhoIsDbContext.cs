using TinfWhoIs.Models;
using Microsoft.EntityFrameworkCore;

namespace TinfWhoIs.Core.Database
{
    public sealed class WhoIsDbContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<TeacherTagVote> TeacherTagVotes { get; set; }

        /// <inheritdoc />
        public WhoIsDbContext()
        {
        }

        /// <inheritdoc />
        public WhoIsDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}