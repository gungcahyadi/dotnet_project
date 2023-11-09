using System.ComponentModel.DataAnnotations;

namespace project3.Models.Users
{
    public class CreateRequest
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [MinLength(6)]
        public string? Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }
    }
}