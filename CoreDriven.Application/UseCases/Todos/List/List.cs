using CoreDriven.Application.Common;
using CoreDriven.Application.Common.Pagination;
using CoreDriven.Application.Common.UseCases;

namespace CoreDriven.Application.UseCases.Todos.List;

public class Request : PaginationRequest { }

public record Response(string Id, string Name);

public class List: IUseCase<Request, PaginationResponse<Response>>
{
    public async Task<PaginationResponse<Response>> ExecuteAsync(Request request)
    {
        var records = new List<Response>
        {
            new("1", "Todo 1"),
            new("2", "Todo 2"),
            new("3", "Todo 3")
        };

        var paginated  = records.ToPaginatedResponse(records.Count, request.ItemsPerPage, request.CurrentPage);
        return await Task.FromResult(paginated);
    }
}