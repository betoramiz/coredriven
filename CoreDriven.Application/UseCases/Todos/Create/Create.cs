using CoreDriven.Application.Common;
using CoreDriven.Domain;

namespace CoreDriven.Application.UseCases.Todos.Create;

public class Create: IUseCase
{
    private readonly IDataBase _dataBase;

    public Create(IDataBase dataBase)
    {
        _dataBase = dataBase;
    }
    
    public Response Execute(Request request)
    {
        var newTodo = new Todo(request.Name);
        var result = _dataBase.Create(newTodo);
        
        return new Response(result);
    }
}