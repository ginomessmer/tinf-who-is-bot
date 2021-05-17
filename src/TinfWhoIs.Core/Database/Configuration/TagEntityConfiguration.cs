using TinfWhoIs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace TinfWhoIs.Core.Database.Configuration
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

            yield return new Tag(i++, "Qualit�t", "Wie ist die Organisation der Materialien und die Struktur der Vorlesung?");
            yield return new Tag(i++, "Tempo", "Wie ist das Tempo der Vorlesung?");
            yield return new Tag(i++, "�bungsaufgaben", "Werden gute �bungsaufgaben bereitgestellt?");
            yield return new Tag(i++, "Wiederholungen", "Finden ausreichende Wiederholungen des Inhalts statt?");
            yield return new Tag(i++, "Engagiert", "Wirkt die Person an andere Projekte oder sonstige Aktivit�ten mit?");
            yield return new Tag(i++, "Offenheit", "Ist die Person offen f�r Feedback und Vorschl�ge? Werden Studenten aktiv miteinbezogen?");
            yield return new Tag(i++, "Zuverl�sslichkeit bei Emails", "Darunter z�hlen bspw. die Reaktionszeit, die Qualit�t der Antwort, etc.");
            yield return new Tag(i++, "Altklausuren", "Sind Altklausuren vorhanden, die entweder von der Person oder durch �ltere Jahrg�nge bereitgestellt wurden?");
            yield return new Tag(i++, "Klausuren", "Ist das Niveau der Klausuren angemessen?");
            yield return new Tag(i++, "Klausurkorrektur", "Wie schnell werden Klausuren korrigiert?");
        }
    }
}