using System.ComponentModel.DataAnnotations;

namespace project3.Models.Users
{
    public class UpdateRequest
    {
        public string? Name { get; set; }
        public string? Username { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

        private string? _password;
        public string? Password
        {
            get => _password;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _password = value;
                }
            }
        }

        private string? _confirmPassword;

        [Compare(nameof(Password))]
        public string? ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _confirmPassword = value;
                }
            }
        }        
    }
}