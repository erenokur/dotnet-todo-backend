namespace dotnet_todo_backend.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using dotnet_todo_backend.Entities;
using dotnet_todo_backend.Helpers;
using dotnet_todo_backend.Models;
using dotnet_todo_backend.interfaces;

public class UserService : IUserService
{
    private List<User> _users = new List<User>
    {
        new User { id = "1", username = "test", email = "User", password = "test", created = DateTime.Now}
    };

    private readonly AppSettings _appSettings;

    public UserService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public AuthenticateResponse? Authenticate(AuthenticateRequest model)
    {
        var user = _users.SingleOrDefault(x => x.username == model.Username && x.password == model.Password);

        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        // serialize user and token to JSON strings
        string userJson = JsonSerializer.Serialize(user);
        string tokenJson = JsonSerializer.Serialize(token);

        AuthenticateResponse response = new AuthenticateResponse
        {
            id = user.id,
            username = user.username,
            email = user.email,
            password = user.password,
            created = tokenJson
        };
        return response;
    }

    public IEnumerable<User> GetAll()
    {
        return _users;
    }

    public User? GetById(string id)
    {
        return _users.FirstOrDefault(x => x.id == id);
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