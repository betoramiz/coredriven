using Microsoft.AspNetCore.Identity;

namespace CoreDriven.Application.Common.Token;

public interface ITokenGenerator
{
    string GenerateToken(IdentityUser user, IEnumerable<string> roles);
    string GenerateRefreshToken();
}