namespace dotnet_todo_backend.Models;

using System.ComponentModel.DataAnnotations;

public class AuthenticateRequest
{
    [Required]
    public string email { get; set; } = String.Empty;

    [Required]
    public string password { get; set; } = String.Empty;
}