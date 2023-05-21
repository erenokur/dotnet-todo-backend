namespace dotnet_todo_backend.Entities;

using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Username { get; set; } = String.Empty;

    [JsonIgnore]
    public string Password { get; set; } = String.Empty;
}