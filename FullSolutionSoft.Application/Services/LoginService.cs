using FullSolutionSoft.Application.Authentication;
using FullSolutionSoft.Application.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace FullSolutionSoft.Application.Services;

public class LoginService : ILoginService
{
    private readonly string _jwtSecret;
    private readonly string _jwtIssuer;
    private readonly string _jwtAudience;

    public LoginService(IConfiguration config)
    {
        // In real app, load from appsettings or environment variables
        _jwtSecret = config["JwtSettings:Secret"];
        _jwtIssuer = config["JwtSettings:Issuer"];
        _jwtAudience = config["JwtSettings:Audience"];
    }

    public async Task<LoginResult?> LoginAsync(LoginRequest request)
    {
        // TODO: Replace with actual DB validation
        if (request.Username != "admin" || request.Password != "password")
            return null;

        var key = Encoding.UTF8.GetBytes(_jwtSecret);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, "Admin")
            }),
            Expires = DateTime.Now.AddHours(2),
            Issuer = _jwtIssuer,
            Audience = _jwtAudience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return new LoginResult(jwt);
    }
}
