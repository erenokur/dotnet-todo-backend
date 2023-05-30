namespace dotnet_todo_backend.Entities;

using System.Text.Json.Serialization;

public class User
{
    public int id { get; set; }
    public string username { get; set; } = String.Empty;
    public string email { get; set; } = String.Empty;
    public string password { get; set; } = String.Empty;

    [JsonIgnore]
    public DateTime created { get; set; }
}