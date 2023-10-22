namespace Application.Feature.Employees.InteractionWithProject
{
    public record RemoveEmployeeFromProjectCommand : IRequest<Result>
    {
        public Guid ProjectId { get; init; }
        public Guid EmployeeId { get; init; }
    }

    public class RemoveEmployeeFromProjectValidatpr : AbstractValidator<RemoveEmployeeFromProjectCommand>
    {
        public RemoveEmployeeFromProjectValidatpr()
        {

            RuleFor(p => p.ProjectId).NotEmpty();
            RuleFor(e => e.EmployeeId).NotEmpty();
        }
    }

    public class RemoveEmployeeFromProjectHandler : IRequestHandler<RemoveEmployeeFromProjectCommand, Result>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveEmployeeFromProjectHandler(IEmployeeRepository employeeRepository, IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result> Handle(RemoveEmployeeFromProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId);
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

            if (project == null || employee == null)
                return Result.Fail("Project or employee not Found");

            employee.MemberProjects!.Remove(project);
            project.ProjectEmployees!.Remove(employee);

            _employeeRepository.Update(employee);
            _projectRepository.Update(project);
            await _unitOfWork.SaveCommitAsync();

            return Result.Ok();
        }
    }
}
