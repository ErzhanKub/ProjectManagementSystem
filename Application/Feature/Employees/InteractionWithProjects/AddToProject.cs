namespace Application.Feature.Employees.InteractionWithProject
{
    public record AddEmployeeToProjectCommand : IRequest<Result>
    {
        public Guid ProjectId { get; init; }
        public Guid EmployeeId { get; init; }
    }

    public class AddEmployeeToProjectValidator : AbstractValidator<AddEmployeeToProjectCommand>
    {
        public AddEmployeeToProjectValidator()
        {
            RuleFor(p => p.ProjectId).NotEmpty();
            RuleFor(e => e.EmployeeId).NotEmpty();
        }
    }

    public class AddEmployeeToProjectHandler : IRequestHandler<AddEmployeeToProjectCommand, Result>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddEmployeeToProjectHandler(IEmployeeRepository employeeRepository, IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result> Handle(AddEmployeeToProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId);
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);


            if (project == null || employee == null)
                return Result.Fail("Project or employee not Found");

            employee.Role = Domain.Enums.Role.Employee;
            employee.MemberProjects!.Add(project);
            project.ProjectEmployees!.Add(employee);

            _employeeRepository.Update(employee);
            _projectRepository.Update(project);
            await _unitOfWork.SaveCommitAsync();

            return Result.Ok();
        }
    }
}
