using System;
using System.Collections.Generic;

namespace TinfWhoIs.Models
{
    public class TeacherTag : Entity<Guid>
    {
        public Tag Tag { get; set; }

        public Teacher Teacher { get; set; }

        public ICollection<TeacherTagVote> Votes { get; set; } = new List<TeacherTagVote>();

        public ulong CreatedBy { get; set; }

        public TeacherTagEvaluation Evaluation { get; set; } = new();

        public TeacherTag(Tag tag)
        {
            Tag = tag;
        }

        public TeacherTag()
        {
        }
    }
}