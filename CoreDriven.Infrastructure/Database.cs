using System.Reflection;
using CoreDriven.Application.Common;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreDriven.Infrastructure;

public class Database: IdentityDbContext<IdentityUser>, IDataBaseAccess
{
    public Database(DbContextOptions<Database> options): base(options) { }
    
    public async Task<ErrorOr<int>> SaveDataAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            // _logger.LogError(message: $"Error saving to database - {e.Message}", exception: e);
            return Error.Failure("Database.SaveError", "An error occurred while saving data to the database");
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
    
    public async Task<ErrorOr<Success>> ExecuteInTransactionAsync(Func<Task> operation)
    {
        await using var transaction = await Database.BeginTransactionAsync();
        try
        {
            await operation();
            await transaction.CommitAsync();
            
            return Result.Success;
        }
        catch(Exception e)
        {
            await transaction.RollbackAsync();

            return Error.Failure("Transaction.Failure", e. Message);
        }
    }
}