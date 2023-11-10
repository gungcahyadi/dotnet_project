using System.ComponentModel.DataAnnotations;

namespace project3.Models.Tasks
{
    public class UpdateTaskRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }     
    }
}