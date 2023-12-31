using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project3.Models.Users;
using project3.Services;

namespace project3.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
        
    [HttpGet]
    [Authorize]
    public IActionResult All()
    {
        var users = _userService.All();
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult Find(int id)
    {
        var user = _userService.Find(id);
        return Ok(user);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Create(CreateRequest model)
    {
        var user = _userService.Create(model);
        return Ok(new { message = "User created successfully" });

    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult Update(int id, UpdateRequest model)
    {
        _userService.Update(id, model);
        return Ok(new { message = "User updated successfully" });
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult Delete(int id)
    {
        _userService.Delete(id);
        return Ok(new { message = "User deleted successfully" });
    }

}
