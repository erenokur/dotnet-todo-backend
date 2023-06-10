namespace dotnet_todo_backend.Models;

using System.ComponentModel.DataAnnotations;

public class RegisterRequest
{
    [Required]
    public string email { get; set; } = String.Empty;

    [Required]
    public string password { get; set; } = String.Empty;

    [Required]
    public string username { get; set; } = String.Empty;
}