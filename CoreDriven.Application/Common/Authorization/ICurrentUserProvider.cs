namespace CoreDriven.Application.Common.Authorization;

public interface ICurrentUserProvider
{
    CurrentUser GetCurrentUser();
}