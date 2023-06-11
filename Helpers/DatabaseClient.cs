using dotnet_todo_backend.Entities;
using dotnet_todo_backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace dotnet_todo_backend.Helpers;
public class DatabaseClient
{
    private readonly AppSettings _appSettings;
    private readonly IMongoDatabase _databaseClient;

    public DatabaseClient(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
        var client = new MongoClient(_appSettings.ConnectionString);
        var database = client.GetDatabase(_appSettings.DatabaseName);
        _databaseClient = database;
    }

    public IMongoCollection<Users> GetUserCollection()
    {

        return _databaseClient.GetCollection<Users>("users");
    }

    public IMongoCollection<Tasks> GetTaskCollection()
    {
        return _databaseClient.GetCollection<Tasks>("tasks");
    }
}
