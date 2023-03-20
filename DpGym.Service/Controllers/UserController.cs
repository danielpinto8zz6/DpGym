using DpGym.Application.Services;
using DpGym.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DpGym.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    
    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userRegistration)
    {
        var userResult = await _userService.RegisterAsync(userRegistration);
        return !userResult.Succeeded ? BadRequest(userResult) : StatusCode(201);
    }
}