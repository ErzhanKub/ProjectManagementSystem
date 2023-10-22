namespace Application.Feature.Projects.Delete
{
    public record DeleteProjectByIdsCommand : IRequest<Result<Guid[]>>
    {
        public Guid[]? Id { get; init; }
    }
    public class DeleteProjectValidator : AbstractValidator<DeleteProjectByIdsCommand>
    {
        public DeleteProjectValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    public class DeleteProjectHandler : IRequestHandler<DeleteProjectByIdsCommand, Result<Guid[]>>
    {
        private readonly IProjectRepository projectRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeleteProjectHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            this.projectRepository = projectRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid[]>> Handle(DeleteProjectByIdsCommand request, CancellationToken cancellationToken)
        {
            var response = await projectRepository.DeleteRangeAsync(request.Id!);
            if (response != null)
            {
                await unitOfWork.SaveCommitAsync();
                return Result.Ok(response);
            }

            return Result.Fail<Guid[]>("Project not found");
        }
    }
}
