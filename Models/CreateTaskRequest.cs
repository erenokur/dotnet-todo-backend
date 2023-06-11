namespace dotnet_todo_backend.Models;

using System.ComponentModel.DataAnnotations;
using dotnet_todo_backend.interfaces;
public class CreateTaskRequest : ICreateTaskRequest
{
    [Required]
    public string title { get; set; } = String.Empty;
    public string userId { get; set; } = String.Empty;

}