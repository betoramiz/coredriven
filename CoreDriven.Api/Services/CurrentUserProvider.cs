using System.Security.Claims;
using CoreDriven.Application.Common.Authorization;

namespace CoreDriven.Api.Services;

public class CurrentUserProvider(IHttpContextAccessor httpContextAccessor): ICurrentUserProvider
{
    public CurrentUser GetCurrentUser()
    {
        var id= GetClaimValues("id")
            .Select(Guid.Parse)
            .First();
        
        // var permissions = GetClaimValues("permissions");
        var roles = GetClaimValues(ClaimTypes.Role);

        return new CurrentUser(Id: id, Permissions: [], Roles: roles);
    }
    
    private IReadOnlyList<string> GetClaimValues(string claimType) =>
        httpContextAccessor.HttpContext!.User.Claims
            .Where(claim => claim.Type == claimType)
            .Select(claim => claim.Value)
            .ToList();   
}