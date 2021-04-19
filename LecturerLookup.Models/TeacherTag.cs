using System;
using System.Collections.Generic;

namespace LecturerLookup.Models
{
    public class TeacherTag : Entity<Guid>
    {
        public Tag Tag { get; set; }

        public Teacher Teacher { get; set; }

        public ICollection<TeacherTagVote> Votes { get; set; } = new List<TeacherTagVote>();

        public ulong CreatedBy { get; set; }

        public TeacherTag(Tag tag, ulong userId)
        {
            Tag = tag;
            CreatedBy = userId;
        }

        public TeacherTag()
        {
        }
    }
}