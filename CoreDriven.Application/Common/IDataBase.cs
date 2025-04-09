using CoreDriven.Domain;

namespace CoreDriven.Application.Common;

public interface IDataBase
{
    public string Create(Todo todo);
}