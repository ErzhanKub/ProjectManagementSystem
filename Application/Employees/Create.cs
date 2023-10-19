

namespace Application.Employees
{
    public record CreateEmployeeCommand : IRequest<Result<EmployeeDto>>
    {
        public EmployeeDto? EmployeeDto { get; init; }
    }

    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(c => c.EmployeeDto).NotNull();
            When(c => c.EmployeeDto != null,
                () =>
                {
                    RuleFor(c => c.EmployeeDto!.Firstname).NotEmpty().Length(1, 200);
                    RuleFor(c => c.EmployeeDto!.Lastname).NotEmpty().Length(1, 200);
                    RuleFor(c => c.EmployeeDto!.Email).NotEmpty().EmailAddress();
                });
        }
    }

    public class CreateEmployeeHandlar : IRequestHandler<CreateEmployeeCommand, Result<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeHandlar(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository)); ;
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<EmployeeDto>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Firstname = request.EmployeeDto!.Firstname,
                Lastname = request.EmployeeDto.Lastname,
                Email = request.EmployeeDto.Email,

            };

            await _employeeRepository.CreateAsync(employee).ConfigureAwait(false);
            await _unitOfWork.SaveCommitAsync().ConfigureAwait(false);

            var response = employee.Adapt<EmployeeDto>();

            return Result.Ok(response);
        }
    }
}
