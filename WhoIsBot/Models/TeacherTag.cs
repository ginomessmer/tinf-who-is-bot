using System;
using System.Collections.Generic;

namespace WhoIsBot.Models
{
    public class TeacherTag : Entity<Guid>
    {
        public Tag Tag { get; set; }

        public Teacher Teacher { get; set; }

        public ICollection<TeacherTagVote> Votes { get; set; }

        public ulong CreatedBy { get; set; }

        public TeacherTag(Tag tag, ulong userId)
        {
            Tag = tag;
            CreatedBy = userId;

            Votes = new List<TeacherTagVote>
            {
                new(userId)
            };
        }

        public TeacherTag()
        {
        }
    }
}