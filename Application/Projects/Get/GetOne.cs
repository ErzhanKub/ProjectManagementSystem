using Application.Contracts;

namespace Application.Projects.Get
{
    public record GetOneProjectByIdRequest : IRequest<Result<ProjectDto>>
    {
        public Guid Id { get; init; }
    }

    public class GetOneProjectValidator : AbstractValidator<GetOneProjectByIdRequest>
    {
        public GetOneProjectValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    internal class GetOneProjectHandler : IRequestHandler<GetOneProjectByIdRequest, Result<ProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetOneProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public async Task<Result<ProjectDto>> Handle(GetOneProjectByIdRequest request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id).ConfigureAwait(false);
            if (project == null)
                return Result.Fail<ProjectDto>("Project not found");
            return Result.Ok(project.Adapt<ProjectDto>());
        }
    }
}
