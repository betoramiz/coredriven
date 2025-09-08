using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CoreDriven.Application.Common.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CoreDriven.Infrastructure.Authentication;

public class TokenGenerator(IOptions<JwtSettings> jwtOptions): ITokenGenerator
{
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;
    
    public string GenerateToken(IdentityUser user, IEnumerable<string> roles)
    {
        var claims = new List<Claim>()
        {
            new("id", user.Id),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, string.Join(",", roles ?? Array.Empty<string>()))
        };
		
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Value.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            expires: DateTime.UtcNow.AddMinutes(jwtOptions.Value.TokenExpirationInMinutes),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenHandler;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}