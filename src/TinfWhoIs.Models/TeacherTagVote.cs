using System;
using System.ComponentModel.DataAnnotations;

namespace TinfWhoIs.Models
{
    public class TeacherTagVote : Entity<Guid>
    {
        public TeacherTag TeacherTag { get; set; }

        public Guid TeacherTagId { get; set; }

        [Required]
        public ulong VoterId { get; set; }

        [Range(-1, 1)]
        public int Score { get; set; }
        
        public ulong MessageId { get; set; }

        public TeacherTagVote(ulong voterId, int score = 0)
        {
            VoterId = voterId;
            Score = score;
        }
    }
}