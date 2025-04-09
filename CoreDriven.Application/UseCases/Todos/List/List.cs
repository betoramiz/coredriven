using CoreDriven.Application.Common;

namespace CoreDriven.Application.UseCases.Todos.List;

public record Response(string Id, string Name);

public class List: IUseCase
{
    public List<Response> Execute()
    {
        return
        [
            new Response("1", "Todo 1"),
            new Response("2", "Todo 2"),
            new Response("3", "Todo 3")
        ];
    }
}