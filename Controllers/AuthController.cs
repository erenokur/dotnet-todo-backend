using System.Security.Claims;
using dotnet_todo_backend.interfaces;
using dotnet_todo_backend.Models;
using dotnet_todo_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace dotnet_todo_backend.Controllers;

[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private UserService _userService;
    private AppSettings _appSettings;


    public AuthController(ILogger<AuthController> logger, IOptions<AppSettings> appSettings)
    {
        _userService = new UserService(appSettings);
        _logger = logger;
        _appSettings = appSettings.Value;
    }

    [HttpPost("login")]
    public IActionResult login(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest model)
    {
        var response = _userService.Register(model);

        if (response == false)
            return BadRequest(new { message = "Register Failed" });

        return Ok(new { message = "User registered successfully!" });
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [Authorize]
    [HttpGet("getUserData")]
    public IActionResult getUserData()
    {
        var userId = HttpContext.User.FindFirst("id")?.Value;
        var userName = HttpContext.User.FindFirst("username")?.Value;
        if (!string.IsNullOrEmpty(userName))
        {
            return Ok(new { username = userName, userId = userId });
        }
        else
        {
            return BadRequest(new { message = "Username or password is not found" });
        }

    }
}