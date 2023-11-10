using project3.Models.Users;
using project3.Helpers;
using project3.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace project3.Services
{
    public interface IAuthenticateService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }

    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUserService _userService;

        public AuthenticateService(IUserService userService)
        {
            _userService = userService;
        }    

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var username = model.Username;
            var user = _userService.FindByUsername(username);

            // Check if the user exists
            if (user == null)
                throw new AppException("Username not found");

            // Verify the password
            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                throw new AppException("Incorrect password");

            // Generate JWT token using JwtHelper
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmFtZSI6IkphdmFJblVzZSIsImV4cCI6MTY5OTU4NTE4MCwiaWF0IjoxNjk5NTg1MTgwfQ.eALH2Zr7EtE-J1LqMJgAghNJyKi9FedmHiAZVB2Ki_Q");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticateResponse(user, tokenHandler.WriteToken(token));
        }

    }
}
