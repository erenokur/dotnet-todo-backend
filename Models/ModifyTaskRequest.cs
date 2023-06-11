namespace dotnet_todo_backend.Models;

using System.ComponentModel.DataAnnotations;
using dotnet_todo_backend.interfaces;
public class ModifyTaskRequest : IModifyTaskRequest
{
    [Required]
    public string id { get; set; } = String.Empty;
    public string userId { get; set; } = String.Empty;
    public string ModifyType { get; set; } = String.Empty;
    public bool ModifyValue { get; set; } = false;

}