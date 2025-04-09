using CoreDriven.Application.Common;
using CoreDriven.Domain;

namespace CoreDriven.Infrastructure;

public class Database: IDataBase
{
    public string Create(Todo todo)
    {
        return todo.Id;
    }
}