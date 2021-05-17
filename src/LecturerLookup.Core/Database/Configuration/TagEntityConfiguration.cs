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

            yield return new Tag(i++, "Qualität", "Wie ist die Organisation der Materialien und die Struktur der Vorlesung?");
            yield return new Tag(i++, "Tempo", "Wie ist das Tempo der Vorlesung?");
            yield return new Tag(i++, "Übungsaufgaben", "Werden gute Übungsaufgaben bereitgestellt?");
            yield return new Tag(i++, "Wiederholungen", "Finden ausreichende Wiederholungen des Inhalts statt?");
            yield return new Tag(i++, "Engagiert", "Wirkt die Person an andere Projekte oder sonstige Aktivitäten mit?");
            yield return new Tag(i++, "Offenheit", "Ist die Person offen für Feedback und Vorschläge? Werden Studenten aktiv miteinbezogen?");
            yield return new Tag(i++, "Zuverlässlichkeit bei Emails", "Darunter zählen bspw. die Reaktionszeit, die Qualität der Antwort, etc.");
            yield return new Tag(i++, "Altklausuren", "Sind Altklausuren vorhanden, die entweder von der Person oder durch ältere Jahrgänge bereitgestellt wurden?");
            yield return new Tag(i++, "Klausuren", "Ist das Niveau der Klausuren angemessen?");
            yield return new Tag(i++, "Klausurkorrektur", "Wie schnell werden Klausuren korrigiert?");
        }
    }
}