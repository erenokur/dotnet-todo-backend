using dotnet_todo_backend.Entities;
using dotnet_todo_backend.Helpers;
using dotnet_todo_backend.interfaces;
using dotnet_todo_backend.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using BCrypt.Net;

namespace dotnet_todo_backend.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly AppSettings _appSettings;
        private readonly JwtHelper _jwtHelper;
        private readonly DatabaseClient _databaseClient;
        public UserService(IOptions<AppSettings> appSettings)
        {
            _databaseClient = new DatabaseClient(appSettings);
            _users = _databaseClient.GetUserCollection();
            _jwtHelper = new JwtHelper(appSettings);
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse? Authenticate(AuthenticateRequest model)
        {
            var user = _users.Find(x => x.email == model.email).SingleOrDefault();
            if (user == null) return null;

            bool result = BCrypt.Net.BCrypt.Verify(model.password, user.password);
            if (!result) return null;

            // authentication successful so generate jwt token
            var tokenJson = _jwtHelper.generateJwtToken(user);

            // serialize user and token to JSON strings
            string userJson = JsonSerializer.Serialize(user);

            AuthenticateResponse response = new AuthenticateResponse
            {
                id = user.id,
                username = user.username,
                token = tokenJson
            };
            return response;
        }
        public RegisterRequest? Register(RegisterRequest model)
        {
            var user = _users.Find(x => x.email == model.email).SingleOrDefault();
            if (user != null) return null;

            // hash password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.password);

            // create user
            User newUser = new User
            {
                email = model.email,
                password = hashedPassword,
                username = model.username,
                created = DateTime.Now
            };

            // insert user
            _users.InsertOne(newUser);

            // return user without password
            return new RegisterRequest
            {
                email = newUser.email,
                username = newUser.username
            };
        }

        public IEnumerable<User> GetAll()
        {
            return _users.Find(x => true).
            Project<User>(Builders<User>.Projection.Exclude(u => u.password))
            .ToList();
        }

        public User? GetById(string id)
        {
            return _users.Find(x => x.id == id).SingleOrDefault();
        }
    }
}
