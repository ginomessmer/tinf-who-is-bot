using LecturerLookup.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace LecturerLookup.Core.Database.Configuration
{
    public class TagEntityConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasData(Seed());

            builder.HasMany(x => x.TeacherTags)
                .WithOne(x => x.Tag);
        }

        private static IEnumerable<Tag> Seed()
        {
            var i = 1;

            yield return new Tag(i++, "Reagiert schnell auf Emails");
            yield return new Tag(i++, "Gut strukturiert");
            yield return new Tag(i++, "Stellt Altklausuren bereit");
            yield return new Tag(i++, "Fleiß");
            yield return new Tag(i++, "Offen für Feedback");
            yield return new Tag(i++, "Gruppenarbeiten");
            yield return new Tag(i++, "Wiederholt Inhalte");
            yield return new Tag(i++, "Schnelle Klausurkorrektur");
        }
    }
}