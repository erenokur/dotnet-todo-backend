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
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }
}