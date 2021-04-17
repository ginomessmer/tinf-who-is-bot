using System;
using System.ComponentModel.DataAnnotations;

namespace WhoIsBot.Models
{
    public class TeacherTagVote : Entity<Guid>
    {
        public TeacherTag TeacherTag { get; set; }

        public ulong VoterId { get; set; }

        public TeacherTagVote(ulong voterId)
        {
            VoterId = voterId;
        }
    }
}