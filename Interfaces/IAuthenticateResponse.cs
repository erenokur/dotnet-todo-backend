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
    int Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string Username { get; set; }
    string Token { get; set; }
}