using TinfWhoIs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TinfWhoIs.Core.Database.Configuration
{
    public class TeacherTagVoteEntityConfiguration : IEntityTypeConfiguration<TeacherTagVote>
    {
        public void Configure(EntityTypeBuilder<TeacherTagVote> builder)
        {
            builder.HasOne(x => x.TeacherTag)
                .WithMany(x => x.Votes);

            builder.HasIndex(x => new {x.TeacherTagId, x.VoterId})
                .IsUnique();
        }
    }
}