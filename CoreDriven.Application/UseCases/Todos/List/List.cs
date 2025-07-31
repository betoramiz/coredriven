using CoreDriven.Application.Common;

namespace CoreDriven.Application.UseCases.Todos.List;

public record Response(string Id, string Name);

public class List: IUseCase<List<Response>>
{
    public async Task<List<Response>> ExecuteAsync()
    {
        return await Task.FromResult(
            new List<Response>
            {
                new("1", "Todo 1"),
                new ("2", "Todo 2"),
                new ("3", "Todo 3")
            }
        );
    }
}