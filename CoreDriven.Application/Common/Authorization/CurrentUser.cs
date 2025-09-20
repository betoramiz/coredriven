namespace CoreDriven.Application.Common.Authorization;

public record CurrentUser(Guid Id, IReadOnlyList<string> Permissions, IReadOnlyList<string> Roles);