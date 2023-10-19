namespace Application.Employees
{
    public record UpdateEmployeeProfileByIdCommand : IRequest<Result<EmployeeProfileDto>>
    {
        public EmployeeProfileDto? EmployeeDto { get; init; }
    }

    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeProfileByIdCommand>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(c => c.EmployeeDto).NotNull();
            When(c => c.EmployeeDto != null,
                () =>
                {
                    RuleFor(c => c.EmployeeDto!.Firstname).NotEmpty().Length(1, 200);
                    RuleFor(c => c.EmployeeDto!.Lastname).NotEmpty().Length(1, 200);
                    RuleFor(c => c.EmployeeDto!.Patronymic).Length(1,200);
                    RuleFor(c => c.EmployeeDto!.Email).NotEmpty().EmailAddress();
                });
        }
    }

    public class UpdateEmployeeProfileHandler : IRequestHandler<UpdateEmployeeProfileByIdCommand, Result<EmployeeProfileDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeProfileHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<EmployeeProfileDto>> Handle(UpdateEmployeeProfileByIdCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeDto!.Id);

            if (employee is null)
                return Result.Fail<EmployeeProfileDto>("Employee not found");

            employee.Firstname = request.EmployeeDto.Firstname;
            employee.Lastname = request.EmployeeDto.Lastname;
            employee.Patronymic = request.EmployeeDto.Patronymic;
            employee.Email = request.EmployeeDto.Email;
            employee.Role = request.EmployeeDto.Role;

             _employeeRepository.Update(employee);
            await _unitOfWork.SaveCommitAsync();

            var response = employee.Adapt<EmployeeProfileDto>();

            return Result.Ok(response);
        }
    }
}
