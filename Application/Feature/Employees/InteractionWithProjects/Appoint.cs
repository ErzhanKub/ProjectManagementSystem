namespace Application.Feature.Employees.InteractionWithProject
{
    public class AppointEmployeeCommand : IRequest<Result>
    {
        public Guid ProjectId { get; init; }
        public Guid EmployeeId { get; init; }
    }

    public class AppointEmployeeValidator : AbstractValidator<AppointEmployeeCommand>
    {
        public AppointEmployeeValidator()
        {
            RuleFor(p => p.ProjectId).NotEmpty();
            RuleFor(e => e.EmployeeId).NotEmpty();
        }
    }

    public class AppointEmployeeHandler : IRequestHandler<AppointEmployeeCommand, Result>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AppointEmployeeHandler(IProjectRepository projectRepository, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result> Handle(AppointEmployeeCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId);
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);


            if (project == null || employee == null)
                return Result.Fail("Project or employee not Found");

            project.ProjectManagerId = employee.Id;
            project.ProjectManager = employee;
            employee.Role = Domain.Enums.Role.ProjectManager;

            _employeeRepository.Update(employee);
            _projectRepository.Update(project);
            await _unitOfWork.SaveCommitAsync();

            return Result.Ok();
        }
    }
}
