using System;
using System.ComponentModel.DataAnnotations;

namespace WhoIsBot.Models
{
    public class TeacherTagVote : Entity<Guid>
    {
        public TeacherTag TeacherTag { get; set; }

        [Required]
        public ulong VoterId { get; set; }

        [Range(-1, 1)]
        public int Score { get; set; }

        public TeacherTagVote(ulong voterId, int score = 0)
        {
            VoterId = voterId;
            Score = score;
        }
    }
}