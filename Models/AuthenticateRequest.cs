namespace dotnet_todo_backend.Models;

using System.ComponentModel.DataAnnotations;

public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; } = String.Empty;

    [Required]
    public string Password { get; set; } = String.Empty;
}