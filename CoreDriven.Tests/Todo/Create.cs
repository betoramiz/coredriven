using CoreDriven.Application.Common;
using CoreDriven.Application.Common.Validation;
using FluentValidation;
using FluentValidation.Results;
using Todos = CoreDriven.Application.UseCases.Todos.Create;
using Moq;

namespace CoreDriven.Tests.Todo;

public class Create
{
    private readonly Mock<IDataBase> _dataBaseMock;
    
    public Create()
    {
        _dataBaseMock = new Mock<IDataBase>();
    }
    
    [Fact]
    public async Task Test1()
    {
        var guid = Guid.NewGuid().ToString();
        var request = new Todos.Request("Todo 1");
        var validatorMock = new Mock<IValidator<Todos.Request>>();
        
        _dataBaseMock.Setup(x => x.Create(It.IsAny<Domain.Todo>())).Returns(guid);
        validatorMock.Setup(x => x.Validate(It.IsAny<Todos.Request>())).Returns(new ValidationResult());
        var sut = new Todos.Create(_dataBaseMock.Object, validatorMock.Object);
        
        var result = await sut.ExecuteAsync(request);
        var success = result.Value;
        Assert.Equal(success.Id, guid);
    }
}