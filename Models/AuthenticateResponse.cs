namespace dotnet_todo_backend.Models;

using dotnet_todo_backend.interfaces;
public class AuthenticateResponse : IAuthenticateResponse
{
    public string id { get; set; } = String.Empty;
    public string username { get; set; } = String.Empty;
    public string user { get; set; } = String.Empty;
    public string accessToken { get; set; } = String.Empty;
    public string message { get; set; } = String.Empty;
}