namespace dotnet_todo_backend.Models;

using dotnet_todo_backend.interfaces;
public class AuthenticateResponse : IAuthenticateResponse
{
    public string id { get; set; } = String.Empty;
    public string username { get; set; } = String.Empty;
    public string token { get; set; } = String.Empty;
}