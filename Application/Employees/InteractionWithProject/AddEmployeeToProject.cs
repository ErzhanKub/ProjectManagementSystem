namespace Application.Employees.InteractionWithProject
{
    public class AddEmployeeToProjectCommand : IRequest<Result>
    {
        public Guid ProjectId { get; init; }
        public Guid EmployeeId { get; init; }
    }

    public class AddEmployeeToProjectValidator : AbstractValidator<AddEmployeeToProjectCommand>
    {
        public AddEmployeeToProjectValidator() { }
    }

    public class AddEmployeeToProjectHandler : IRequestHandler<AddEmployeeToProjectCommand, Result>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddEmployeeToProjectHandler(IEmployeeRepository employeeRepository, IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddEmployeeToProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId);
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

            if (project == null || employee == null)
                return Result.Fail("Not Found");
            project.ProjectEmployees.Add(employee);
            _projectRepository.Update(project);
           await _unitOfWork.SaveCommitAsync();
            return Result.Ok();
        }
    }
}
