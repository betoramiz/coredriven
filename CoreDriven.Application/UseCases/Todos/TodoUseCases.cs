using CoreDriven.Application.Common;
using CoreDriven.Application.Common.UseCases;

namespace CoreDriven.Application.UseCases.Todos;

public record TodoUseCases(
    Create.Create Create, 
    List.List List): IUseCaseRepository;