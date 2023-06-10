using dotnet_todo_backend.Entities;
using dotnet_todo_backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace dotnet_todo_backend.Helpers;
public class DatabaseClient
{
    private readonly AppSettings _appSettings;

    public DatabaseClient(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public IMongoCollection<User> GetUserCollection()
    {
        var client = new MongoClient(_appSettings.ConnectionString);
        var database = client.GetDatabase(_appSettings.DatabaseName);
        return database.GetCollection<User>("users");
    }
}
