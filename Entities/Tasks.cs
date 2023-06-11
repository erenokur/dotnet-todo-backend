namespace dotnet_todo_backend.Entities;

using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
public class Tasks
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; } = String.Empty;
    public string userId { get; set; } = String.Empty;
    public string title { get; set; } = String.Empty;
    public bool completed { get; set; } = false;
    public bool active { get; set; } = false;

    [JsonIgnore]
    public DateTime createDate { get; set; }
    [JsonIgnore]
    public DateTime modifyDate { get; set; }
}