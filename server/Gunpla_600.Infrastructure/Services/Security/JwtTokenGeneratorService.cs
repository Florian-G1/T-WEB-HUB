using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Gunpla_600.Application.Interfaces.Security;
using Microsoft.Extensions.Options;


namespace Gunpla_600.Infrastructure.Services.Security;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateToken(int Id, string Email)
    {
        var Claims = new List<Claims>
        {
            new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
            new Claim(ClaimTypes.Email, Email)
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var Key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(_jwtSettings.Secret)
        );
        var Credentials = new SigningCredentials(
            Key, SecurityAlgorithms.HmacSha256
        );
        
        var Token = new JwtSecurityToken(            
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(Token);
    }
}