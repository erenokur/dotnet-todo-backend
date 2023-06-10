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

public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private UserService _userService;
    private AppSettings _appSettings;


    public UserController(ILogger<UserController> logger, IOptions<AppSettings> appSettings)
    {
        _userService = new UserService(appSettings);
        _logger = logger;
        _appSettings = appSettings.Value;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
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

        if (response == null)
            return BadRequest(new { message = "Email exist" });

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [Authorize]
    [HttpGet("whoami")]
    public IActionResult WhoAmI()
    {
        var userId = HttpContext.User.FindFirst("id")?.Value;
        var userName = HttpContext.User.FindFirst("username")?.Value;
        if (!string.IsNullOrEmpty(userName))
        {
            return Ok(new { user = userName, userId = userId });
        }
        else
        {
            return BadRequest(new { message = "Username or password is not found" });
        }

    }
}