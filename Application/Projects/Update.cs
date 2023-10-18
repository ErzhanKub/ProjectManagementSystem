using Application.Contracts;
using Application.Shared;

namespace Application.Projects
{
    public record UpdateProjectByIdCommand : IRequest<Result<ProjectDto>>
    {
        public Guid Id { get; init; }
    }

    public class UpdateProjectValidator : AbstractValidator<UpdateProjectByIdCommand>
    {
        public UpdateProjectValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }

    internal class UpdateProjectHandler : IRequestHandler<UpdateProjectByIdCommand, Result<ProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProjectHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Result<ProjectDto>> Handle(UpdateProjectByIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
