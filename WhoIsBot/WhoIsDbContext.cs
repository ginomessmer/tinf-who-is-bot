using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using WhoIsBot.Models;

namespace WhoIsBot
{
    public class WhoIsDbContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Course> Courses { get; set; }

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
            modelBuilder.Entity<Teacher>().HasData(GetTeachers());

            modelBuilder.Entity<Teacher>()
                .HasMany(x => x.Courses)
                .WithMany(x => x.Teachers);

            base.OnModelCreating(modelBuilder);
        }

        private IEnumerable<Teacher> GetTeachers()
        {
            var web = new HtmlWeb();
            var doc = web.Load("https://www.karlsruhe.dhbw.de/dhbw-karlsruhe/ansprechpersonen/alle.html");
            var panels = doc.DocumentNode.SelectNodes("/html/body/main/div/div[3]/div/div[2]/div/div/div/div/div");

            var contacts = panels.Select(x => new Teacher
            {
                Name = x.SelectSingleNode("div[1]/h4/a").GetDirectInnerText().Trim(),
                Office = x.SelectSingleNode("div[1]/h4/a/div").InnerText.Trim(),
                Telephone = x.SelectSingleNode("div[2]/div/div/div/div[1]/div/div[2]/dl[1]/dd/a")?.GetDirectInnerText().Trim(),
                Email = x.SelectSingleNode("div[2]/div/div/div/div[1]/div/div[2]/dl[2]/dd/a")?.GetDirectInnerText().Trim(),
                Location = x.SelectSingleNode("div[2]/div/div/div/div[1]/div/div[1]/p")?.GetDirectInnerText().Trim(),
                AvatarUrl = x.SelectSingleNode("div[2]/div/div/div/div[2]/div/img")?.GetAttributeValue("src", "").Insert(0, "https://www.karlsruhe.dhbw.de/")
            });

            return contacts;
        }
    }
}