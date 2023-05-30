using dotnet_todo_backend.Entities;
using dotnet_todo_backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace dotnet_todo_backend.Services;
public class MongoDBService
{
    private readonly IMongoCollection<User> _users;
    public MongoDBService(IOptions<MongoDBSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase("dotnet_todo_backend");
        _users = database.GetCollection<User>("users");
    }
    public List<User> Get() =>
        _users.Find(user => true).ToList();
    public User Get(string id) =>
        _users.Find<User>(user => user.id == id).FirstOrDefault();
    public User Create(User user)
    {
        _users.InsertOne(user);
        return user;
    }
    public void Update(string id, User userIn) =>
        _users.ReplaceOne(user => user.id == id, userIn);
    public void Remove(User userIn) =>
        _users.DeleteOne(user => user.id == userIn.id);
    public void Remove(string id) =>
        _users.DeleteOne(user => user.id == id);
}
