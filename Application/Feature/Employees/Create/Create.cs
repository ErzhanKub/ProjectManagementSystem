namespace Application.Feature.Employees.Create
{
    public record CreateEmployeeCommand : IRequest<Result<CreateEmployeeDto>>
    {
        public CreateEmployeeDto? Employee { get; init; }
    }

    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(c => c.Employee).NotNull();
            When(c => c.Employee != null,
                () =>
                {
                    RuleFor(c => c.Employee!.Firstname).NotEmpty().Length(1, 200);
                    RuleFor(c => c.Employee!.Lastname).NotEmpty().Length(1, 200);
                    RuleFor(c => c.Employee!.Password).NotEmpty().Length(1, 200);
                    RuleFor(c => c.Employee!.Email).NotEmpty().EmailAddress();
                });
        }
    }

    public class CreateEmployeeHandlar : IRequestHandler<CreateEmployeeCommand, Result<CreateEmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeHandlar(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<CreateEmployeeDto>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Firstname = request.Employee!.Firstname,
                Lastname = request.Employee.Lastname,
                Patronymic = request.Employee.Patronymic!,
                Email = request.Employee.Email,
                PasswordHash = request.Employee.Password,
                Role = request.Employee.Role,
            };

            await _employeeRepository.CreateAsync(employee);
            await _unitOfWork.SaveCommitAsync();

            return Result.Ok(request.Employee);
        }
    }
}
