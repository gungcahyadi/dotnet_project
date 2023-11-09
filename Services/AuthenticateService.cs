using project3.Models.Users;
using project3.Helpers;
using project3.Responses;

namespace project3.Services;

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

        // Create and return authentication response
        return new AuthenticateResponse(user);
    }
}
