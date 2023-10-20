﻿namespace Application.Projects
{
    public record UpdateProjectByIdCommand : IRequest<Result<FullProjectDto>>
    {
        public FullProjectDto? ProjectDto { get; init; }
    }

    public class UpdateProjectValidator : AbstractValidator<UpdateProjectByIdCommand>
    {
        public UpdateProjectValidator()
        {
        }
    }

    internal class UpdateProjectHandler : IRequestHandler<UpdateProjectByIdCommand, Result<FullProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProjectHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Result<FullProjectDto>> Handle(UpdateProjectByIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
