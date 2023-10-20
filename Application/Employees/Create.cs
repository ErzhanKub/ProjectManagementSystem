﻿namespace Application.Employees
{
    public record CreateEmployeeCommand : IRequest<Result<EmployeeProfileDto>>
    {
        public EmployeeProfileDto? EmployeeDto { get; init; }
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
                    RuleFor(c => c.EmployeeDto!.PasswordHash).NotEmpty().Length(1, 200);
                    RuleFor(c => c.EmployeeDto!.Email).NotEmpty().EmailAddress();
                });
        }
    }

    public class CreateEmployeeHandlar : IRequestHandler<CreateEmployeeCommand, Result<EmployeeProfileDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeHandlar(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<EmployeeProfileDto>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Firstname = request.EmployeeDto!.Firstname,
                Lastname = request.EmployeeDto.Lastname,
                Patronymic = request.EmployeeDto!.Patronymic,
                Email = request.EmployeeDto.Email,
                PasswordHash = request.EmployeeDto.PasswordHash,
                Role = request.EmployeeDto.Role,
            };

            await _employeeRepository.CreateAsync(employee);
            await _unitOfWork.SaveCommitAsync();

            var response = employee.Adapt<EmployeeProfileDto>();

            return Result.Ok(response);
        }
    }
}
