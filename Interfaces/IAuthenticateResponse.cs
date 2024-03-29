namespace dotnet_todo_backend.interfaces;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using dotnet_todo_backend.Entities;
using dotnet_todo_backend.Helpers;
using dotnet_todo_backend.Models;

public interface IAuthenticateResponse
{
    string id { get; set; }
    string username { get; set; }
    string user { get; set; }
    string accessToken { get; set; }
    string message { get; set; }
}
