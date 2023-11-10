using project3.Entities;

namespace project3.Responses
{
    public class AuthenticateResponse
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {

            if (user != null)
            {
                Id = user.Id.ToString();
                Name = user.Name;
                Username = user.Username;
                Email = user.Email;
            }
            Token = token;
        }
    }
}