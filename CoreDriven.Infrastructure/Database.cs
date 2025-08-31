using System.Reflection;
using CoreDriven.Application.Common;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CoreDriven.Infrastructure;

public class Database: DbContext, IDataBaseAccess
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
}