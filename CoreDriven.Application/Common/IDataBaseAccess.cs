using ErrorOr;

namespace CoreDriven.Application.Common;

public interface IDataBaseAccess
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<ErrorOr<int>> SaveDataAsync(CancellationToken cancellationToken = default);
}