using HtmlAgilityPack;
using TinfWhoIs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Linq;

namespace TinfWhoIs.Core.Database.Configuration
{
    public class TeacherEntityConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasData(Seed());

            builder.HasMany(x => x.Courses)
                .WithMany(x => x.Teachers);

        }

        private static IEnumerable<Teacher> Seed()
        {
            var web = new HtmlWeb();
            var doc = web.Load("https://www.karlsruhe.dhbw.de/dhbw-karlsruhe/ansprechpersonen/alle.html");
            var panels = doc.DocumentNode.SelectNodes("/html/body/main/div/div[3]/div/div[2]/div/div/div/div/div");

            var i = 1;

            var contacts = panels.Select(x =>
            {
                var name = x.SelectSingleNode("div[1]/h4/a").GetDirectInnerText().Trim();

                return new Teacher
                {
                    Id = i++,
                    Name = name,
                    Office = x.SelectSingleNode("div[1]/h4/a/div").InnerText.Trim(),
                    Telephone = x.SelectSingleNode("div[2]/div/div/div/div[1]/div/div[2]/dl[1]/dd/a")
                        ?.GetDirectInnerText().Trim(),
                    Email = x.SelectSingleNode("div[2]/div/div/div/div[1]/div/div[2]/dl[2]/dd/a")?.GetDirectInnerText()
                        .Trim(),
                    Location =
                        x.SelectSingleNode("div[2]/div/div/div/div[1]/div/div[1]/p")?.GetDirectInnerText().Trim(),
                    AvatarUrl = x.SelectSingleNode("div[2]/div/div/div/div[2]/div/img")?.GetAttributeValue("src", "")
                        .Insert(0, "https://www.karlsruhe.dhbw.de/"),
                    IsApproved = true
                };
            });

            return contacts;
        }

        private static string Sanitize(string input) => input
            .Replace(",", "")
            .Replace(".", "")
            .Replace("  ", " ");
    }
}