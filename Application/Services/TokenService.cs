using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.IServices;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _signingKey;

    public TokenService(IConfiguration config)
    {
        _config = config;
        _signingKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:SigningKey"]!)
            );
    }

    public string GenerateToken(AppUser appUser)
    {
        List<Claim> claims = [
            new Claim(JwtRegisteredClaimNames.Name, appUser.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, appUser.UserName)
        ];

        var credentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = credentials,
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
