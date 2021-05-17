using TinfWhoIs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TinfWhoIs.Core.Database.Configuration
{
    public class TeacherTagEntityConfiguration : IEntityTypeConfiguration<TeacherTag>
    {
        public void Configure(EntityTypeBuilder<TeacherTag> builder)
        {
            builder.HasOne(x => x.Tag)
                .WithMany(x => x.TeacherTags);

            builder.HasOne(x => x.Teacher)
                .WithMany(x => x.Tags);

            builder.OwnsOne(x => x.Evaluation, navigationBuilder =>
            {
                navigationBuilder.WithOwner();
            });
        }
    }
}