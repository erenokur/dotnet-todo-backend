namespace dotnet_todo_backend.Models;

using dotnet_todo_backend.interfaces;
public class AuthenticateResponse : IAuthenticateResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }
}