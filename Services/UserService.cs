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

        public UserService(IOptions<AppSettings> appSettings)
        {
            var client = new MongoClient(appSettings.Value.ConnectionString);
            var database = client.GetDatabase(appSettings.Value.DatabaseName);
            _users = database.GetCollection<User>("users");

            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse? Authenticate(AuthenticateRequest model)
        {
            var user = _users.Find(x => x.email == model.email).SingleOrDefault();
            if (user == null) return null;

            bool result = BCrypt.Net.BCrypt.Verify(model.password, user.password);
            if (!result) return null;

            // authentication successful so generate jwt token
            var tokenJson = generateJwtToken(user);

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

        public IEnumerable<User> GetAll()
        {
            return _users.Find(x => true).ToList();
        }

        public User? GetById(string id)
        {
            return _users.Find(x => x.id == id).SingleOrDefault();
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
