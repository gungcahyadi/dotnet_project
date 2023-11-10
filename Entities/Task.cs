using System.ComponentModel.DataAnnotations.Schema;

namespace project3.Entities
{
    [Table("tasks")]
    public class Task
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public User? User { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}