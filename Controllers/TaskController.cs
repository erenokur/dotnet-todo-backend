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

public class TaskController : ControllerBase
{
    private readonly ILogger<TaskController> _logger;
    private TaskService _taskService;
    private AppSettings _appSettings;

    public TaskController(ILogger<TaskController> logger, IOptions<AppSettings> appSettings)
    {
        _taskService = new TaskService(appSettings);
        _logger = logger;
        _appSettings = appSettings.Value;
    }

    [Authorize]
    [HttpGet("getTasks")]
    public IActionResult getTasks()
    {
        var userId = HttpContext.User.FindFirst("id")?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            var response = _taskService.GetTasks(userId);
            return Ok(new { data = response });
        }
        else
        {
            return BadRequest(new { message = "No user id available" });
        }
    }
    [Authorize]
    [HttpPost("markDone")]
    public IActionResult markDone(ModifyTaskRequest model)
    {
        var userId = HttpContext.User.FindFirst("id")?.Value;
        if (string.IsNullOrEmpty(userId))
            return BadRequest(new { message = "No user id available" });
        model.ModifyType = "completed";
        model.ModifyValue = true;
        model.userId = userId;
        var response = _taskService.UpdateTask(model);
        if (response == null)
            return BadRequest(new { message = "Task not found" });

        return Ok(new { data = response });
    }
    [Authorize]
    [HttpPost("markUnDone")]
    public IActionResult markUnDone(ModifyTaskRequest model)
    {
        var userId = HttpContext.User.FindFirst("id")?.Value;
        if (string.IsNullOrEmpty(userId))
            return BadRequest(new { message = "No user id available" });
        model.ModifyType = "completed";
        model.ModifyValue = false;
        model.userId = userId;
        var response = _taskService.UpdateTask(model);
        if (response == null)
            return BadRequest(new { message = "Task not found" });

        return Ok(new { data = response });
    }

    [Authorize]
    [HttpPost("activateTask")]
    public IActionResult activateTask(ModifyTaskRequest model)
    {
        var userId = HttpContext.User.FindFirst("id")?.Value;
        if (string.IsNullOrEmpty(userId))
            return BadRequest(new { message = "No user id available" });
        model.ModifyType = "active";
        model.ModifyValue = true;
        model.userId = userId;
        var response = _taskService.UpdateTask(model);
        if (response == null)
            return BadRequest(new { message = "Task not found" });

        return Ok(new { data = response });
    }
    [Authorize]
    [HttpPost("deActivateTask")]
    public IActionResult deActivateTask(ModifyTaskRequest model)
    {
        var userId = HttpContext.User.FindFirst("id")?.Value;
        if (string.IsNullOrEmpty(userId))
            return BadRequest(new { message = "No user id available" });
        model.ModifyType = "active";
        model.ModifyValue = false;
        model.userId = userId;
        var response = _taskService.UpdateTask(model);
        if (response == null)
            return BadRequest(new { message = "Task not found" });

        return Ok(new { data = response });
    }

    [Authorize]
    [HttpPost("createTask")]
    public IActionResult createTask(CreateTaskRequest model)
    {
        var userId = HttpContext.User.FindFirst("id")?.Value;
        if (string.IsNullOrEmpty(userId))
            return BadRequest(new { message = "No user id available" });
        model.userId = userId;
        var response = _taskService.CreateTask(model);
        if (response == null)
            return BadRequest(new { message = "Something is not right" });

        return Ok(new { data = response });
    }
}
