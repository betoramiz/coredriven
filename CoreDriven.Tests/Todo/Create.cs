using CoreDriven.Application.Common;
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
    public void Test1()
    {
        var guid = Guid.NewGuid().ToString();
        var request = new Todos.Request("Todo 1");

        _dataBaseMock.Setup(x => x.Create(It.IsAny<Domain.Todo>())).Returns(guid);
        var sut = new Todos.Create(_dataBaseMock.Object);

        var result = sut.Execute(request);
        Assert.Equal(result.Id, guid);
    }
}