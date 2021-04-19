using LecturerLookup.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecturerLookup.Core.Database.Configuration
{
    public class TeacherTagVoteEntityConfiguration : IEntityTypeConfiguration<TeacherTagVote>
    {
        public void Configure(EntityTypeBuilder<TeacherTagVote> builder)
        {
            builder.HasOne(x => x.TeacherTag)
                .WithMany(x => x.Votes);

            builder.HasAlternateKey(x => x.MessageId);

            builder.HasIndex(x => new {x.TeacherTagId, x.VoterId})
                .IsUnique();
        }
    }
}