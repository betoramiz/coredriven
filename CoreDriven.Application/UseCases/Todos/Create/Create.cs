using CoreDriven.Application.Common;
using CoreDriven.Application.Common.UseCases;
using CoreDriven.Application.Common.Validation;
using CoreDriven.Domain;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;

namespace CoreDriven.Application.UseCases.Todos.Create;

public class Create: IUseCase<Request, ErrorOr<Response>>
{
    private readonly IDataBase _dataBase;
    private readonly IValidator<Request> _validations;

    public Create(IDataBase dataBase, IValidator<Request> validations)
    {
        _dataBase = dataBase;
        _validations = validations;
    }
    
    public async Task<ErrorOr<Response>> ExecuteAsync(Request request)
    {
        if(_validations.IsNotValid(request, out var errors))
            return errors.GetErrors();
        
        var newTodo = new Todo(request.Name);
        var result = _dataBase.Create(newTodo);
        
        return await Task.FromResult(new Response(result));
    }
}