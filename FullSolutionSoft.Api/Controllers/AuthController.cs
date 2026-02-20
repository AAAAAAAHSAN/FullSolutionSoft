using FullSolutionSoft.Infrastructure.Secutiry;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FullSolutionSoft.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenService _jwtService;

    public AuthController(IJwtTokenService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var jwtSecret = "SuperSecureJWTSecretKeyChangeThisInProd!1234567890";
        var jwtIssuer = "FullSolutionSoft";
        var jwtAudience = "FullSolutionSoftClient";

        // TODO: Replace with user validation from DB (Application Layer)
        if (request.Username == "admin" && request.Password == "password")
        {
            var key = Encoding.UTF8.GetBytes(jwtSecret);
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, request.Username),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "Admin")
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = jwtIssuer,
                Audience = jwtAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Ok(new { token = jwt });
        }

        return Unauthorized();
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}