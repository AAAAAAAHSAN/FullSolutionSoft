//using FullSolutionSoft.Application.Authentication;
//using FullSolutionSoft.Application.DTOs;
//using Microsoft.AspNetCore.Identity.Data;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace FullSolutionSoft.Infrastructure.Authentication;

//public class LoginService : ILoginService
//{
//    private readonly string _jwtSecret;
//    private readonly string _jwtIssuer;
//    private readonly string _jwtAudience;

//    public LoginService()
//    {
//        // In real app, load from appsettings or environment variables
//        _jwtSecret = "SuperSecureJWTSecretKeyChangeThisInProd!1234567890";
//        _jwtIssuer = "FullSolutionSoft";
//        _jwtAudience = "FullSolutionSoftClient";
//    }

//    public async Task<LoginResult?> LoginAsync(Application.DTOs.LoginRequest request)
//    {
//        // TODO: Replace with actual DB validation
//        if (request.Username != "admin" || request.Password != "password")
//            return null;

//        var key = Encoding.UTF8.GetBytes(_jwtSecret);
//        var tokenHandler = new JwtSecurityTokenHandler();
//        var tokenDescriptor = new SecurityTokenDescriptor
//        {
//            Subject = new ClaimsIdentity(new[]
//            {
//                    new Claim(ClaimTypes.Name, request.Username),
//                    new Claim(ClaimTypes.Role, "Admin")
//                }),
//            Expires = DateTime.Now.AddHours(2),
//            Issuer = _jwtIssuer,
//            Audience = _jwtAudience,
//            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//        };

//        var token = tokenHandler.CreateToken(tokenDescriptor);
//        var jwt = tokenHandler.WriteToken(token);

//        return new LoginResult(jwt);
//    }
//}
