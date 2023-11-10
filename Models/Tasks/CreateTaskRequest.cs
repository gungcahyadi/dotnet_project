using System.ComponentModel.DataAnnotations;

namespace project3.Models.Tasks
{
    public class CreateTaskRequest
    {
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}