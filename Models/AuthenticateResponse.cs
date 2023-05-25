namespace dotnet_todo_backend.Models;

using dotnet_todo_backend.interfaces;
public class AuthenticateResponse : IAuthenticateResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Username { get; set; } = String.Empty;
    public string Token { get; set; } = String.Empty;
}