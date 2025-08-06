namespace CoreDriven.Application.Common.UseCases;

public interface IBaseUseCase { }

public interface IUseCase<in T, TU>: IBaseUseCase
{
    Task<TU> ExecuteAsync(T request);
}

public interface IUseCase<TU>: IBaseUseCase
{
    Task<TU> ExecuteAsync();
}