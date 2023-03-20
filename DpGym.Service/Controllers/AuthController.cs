using DpGym.Application.Services;
using DpGym.Contracts.Dtos;
using DpGym.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DpGym.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IAuthService _authService;

    public AuthController(ILogger<UserController> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody] UserLoginDto user)
    {
        try
        {
            var token = await _authService.AuthenticateAsync(user);
            return Ok(new { Token = token });
        }
        catch (AuthenticationException)
        {
            return Unauthorized();
        }
    }
}