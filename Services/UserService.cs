using dotnet_todo_backend.Entities;
using dotnet_todo_backend.Helpers;
using dotnet_todo_backend.interfaces;
using dotnet_todo_backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;

namespace dotnet_todo_backend.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<Users> _users;
        private readonly JwtHelper _jwtHelper;
        private readonly DatabaseClient _databaseClient;
        public UserService(IOptions<AppSettings> appSettings)
        {
            _databaseClient = new DatabaseClient(appSettings);
            _users = _databaseClient.GetUserCollection();
            _jwtHelper = new JwtHelper(appSettings);
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

            AuthenticateResponse response = new()
            {
                id = user.id,
                username = user.username,
                accessToken = tokenJson
            };
            return response;
        }
        public bool Register(RegisterRequest model)
        {
            var user = _users.Find(x => x.email == model.email).SingleOrDefault();
            if (user != null) return false;

            // hash password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.password);

            // create user
            Users newUser = new ()
            {
                email = model.email,
                password = hashedPassword,
                username = model.username,
                created = DateTime.Now
            };

            // insert user
            _users.InsertOne(newUser);

            // return user without password
            return true;
        }

        public IEnumerable<Users> GetAll()
        {
            return _users.Find(x => true).
            Project<Users>(Builders<Users>.Projection.Exclude(u => u.password))
            .ToList();
        }

        public Users? GetById(string id)
        {
            return _users.Find(x => x.id == id).SingleOrDefault();
        }
    }
}
