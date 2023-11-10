using Microsoft.AspNetCore.Mvc;
using project3.Helpers;
using project3.Models.Users;
using project3.Services;

namespace project3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            try
            {
                var response = _authenticateService.Authenticate(model);
                return Ok(response);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
