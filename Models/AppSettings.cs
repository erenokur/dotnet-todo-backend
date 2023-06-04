
namespace dotnet_todo_backend.Models;
public class AppSettings
{
    public string Secret { get; set; } = String.Empty;
    public string ConnectionString { get; set; } = String.Empty;
    public string DatabaseName { get; set; } = String.Empty;

}