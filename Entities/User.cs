using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace project3.Entities
{
    [Table("users")]
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        [JsonIgnore]
        public string? PasswordHash { get; set; }
        // public string? RememberToken { get; set; }
    }
}