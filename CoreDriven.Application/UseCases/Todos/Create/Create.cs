using CoreDriven.Application.Common;
using CoreDriven.Domain;

namespace CoreDriven.Application.UseCases.Todos.Create;

public class Create: IUseCase<Request, Response>
{
    private readonly IDataBase _dataBase;

    public Create(IDataBase dataBase)
    {
        _dataBase = dataBase;
    }
    
    public async Task<Response> ExecuteAsync(Request request)
    {
        var newTodo = new Todo(request.Name);
        var result = _dataBase.Create(newTodo);
        
        return await Task.FromResult(new Response(result));
    }
}