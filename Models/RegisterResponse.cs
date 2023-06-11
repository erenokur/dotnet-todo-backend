namespace dotnet_todo_backend.Models;

using dotnet_todo_backend.interfaces;
public class RegisterResponse : IRegisterResponse
{
    public string id { get; set; } = String.Empty;
    public string username { get; set; } = String.Empty;
    public string email { get; set; } = String.Empty;
    public string accessToken { get; set; } = String.Empty;
}