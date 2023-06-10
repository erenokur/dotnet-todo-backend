using dotnet_todo_backend.Entities;
using dotnet_todo_backend.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dotnet_todo_backend.Helpers;
public class JwtHelper
{
    private readonly AppSettings _appSettings;
    public JwtHelper(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public string generateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var claims = new List<Claim>
            {
                new Claim("id", user.id.ToString()),
                new Claim("username", user.username),
                // Add more claims as needed
            };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


}
