namespace Application.Employees.InteractionWithProject
{
    public class RemoveEmployeeFromProjectCommand : IRequest<Result>
    {
        public Guid ProjectId { get; init; }
        public Guid EmployeeId { get; init; }
    }

    public class RemoveEmployeeFromProjectValidatpr : AbstractValidator<RemoveEmployeeFromProjectCommand>
    {
        public RemoveEmployeeFromProjectValidatpr() { }
    }

    public class RemoveEmployeeFromProjectHandler : IRequestHandler<RemoveEmployeeFromProjectCommand, Result>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveEmployeeFromProjectHandler(IEmployeeRepository employeeRepository, IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveEmployeeFromProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId);
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

            if (project == null || employee == null)
                return Result.Fail("Not Found");
            project.ProjectEmployees.Remove(employee);
            _projectRepository.Update(project);
            await _unitOfWork.SaveCommitAsync();
            return Result.Ok();
        }
    }
}
