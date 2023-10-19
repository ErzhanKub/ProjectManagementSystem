namespace Application.Projects
{
    public record DeleteProjectByIdCommand : IRequest<Result<Guid[]>>
    {
        public Guid[]? Id { get; init; }
    }
    public class DeleteProjectValidator : AbstractValidator<DeleteProjectByIdCommand>
    {
        public DeleteProjectValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    internal class DeleteProjectHandler : IRequestHandler<DeleteProjectByIdCommand, Result<Guid[]>>
    {
        private readonly IProjectRepository projectRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeleteProjectHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            this.projectRepository = projectRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid[]>> Handle(DeleteProjectByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await projectRepository.DeleteByIdAsync(request.Id!);
            if (response != null)
            {
                await unitOfWork.SaveCommitAsync();
                return Result.Ok(response);
            }

            return Result.Fail<Guid[]>("Project not found");
        }
    }
}
