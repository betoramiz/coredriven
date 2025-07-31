using CoreDriven.Application.Common;

namespace CoreDriven.Application.UseCases.Todos;

public record TodoUseCases(Create.Create Create, List.List List): IUseCaseRepository;