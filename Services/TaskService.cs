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
        private readonly DatabaseClient _databaseClient;
        public TaskService(IOptions<AppSettings> appSettings)
        {
            _databaseClient = new DatabaseClient(appSettings);
            _tasks = _databaseClient.GetTaskCollection();
        }
        public IEnumerable<Tasks> GetTasks(string userId)
        {
            var tasks = _tasks.Find(x => x.userId == userId && x.active == true).ToList();
            return tasks;
        }

        public IEnumerable<Tasks>? UpdateTask(ModifyTaskRequest model)
        {
            var task = _tasks.Find(x => x._id == model._id && x.userId == model.userId).SingleOrDefault();
            if (task == null)
            {
                return null;
            }
            task.completed = model.ModifyType == "completed" ? model.ModifyValue : task.completed;
            task.active = model.ModifyType == "active" ? model.ModifyValue : task.active;
            _tasks.ReplaceOne(x => x._id == model._id, task);
            var tasks = _tasks.Find(x => x.userId == task.userId && x.active == true).ToList();
            return tasks;
        }

        public IEnumerable<Tasks>? CreateTask(CreateTaskRequest model)
        {
            var task = new Tasks
            {
                userId = model.userId,
                title = model.title,
                completed = false,
                active = true,
                createDate = DateTime.Now,
                modifyDate = DateTime.Now
            };
            _tasks.InsertOne(task);
            var tasks = _tasks.Find(x => x.userId == task.userId && x.active == true).ToList();
            return tasks;
        }

    }
}