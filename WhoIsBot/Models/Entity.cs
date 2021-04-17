using System.ComponentModel.DataAnnotations;

namespace WhoIsBot.Models
{
    public abstract class Entity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}