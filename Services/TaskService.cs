using dotnet_todo_backend.Entities;
using dotnet_todo_backend.Helpers;
using dotnet_todo_backend.interfaces;
using dotnet_todo_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;

namespace dotnet_todo_backend.Services
{
    public class TaskService
    {
        private readonly IMongoCollection<Tasks> _tasks;
        private readonly AppSettings _appSettings;
        private readonly DatabaseClient _databaseClient;
        public TaskService(IOptions<AppSettings> appSettings)
        {
            _databaseClient = new DatabaseClient(appSettings);
            _tasks = _databaseClient.GetTaskCollection();
            _appSettings = appSettings.Value;
        }

    }
}