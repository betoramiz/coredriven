using CoreDriven.Application.Common;
using CoreDriven.Application.Common.UseCases;
using CoreDriven.Application.Common.Validation;
using CoreDriven.Domain.Todo;
using ErrorOr;
using FluentValidation;

namespace CoreDriven.Application.UseCases.Todos.Create;

public class Create: IUseCase
{
    private readonly IDataBaseAccess _dataBaseAccess;
    private readonly IValidator<Request> _validations;

    public Create(IDataBaseAccess dataBaseAccess, IValidator<Request> validations)
    {
        _dataBaseAccess = dataBaseAccess;
        _validations = validations;
    }
    
    public async Task<ErrorOr<Response>> ExecuteAsync(Request request)
    {
        if(_validations.IsNotValid(request, out var errors))
            return errors.GetErrors();
        
        var newTodo = new Todo(request.Name);
        // var result = await _dataBase.SaveDataAsync(newTodo);
        
        return await Task.FromResult(new Response(Guid.NewGuid().ToString()));
    }
}