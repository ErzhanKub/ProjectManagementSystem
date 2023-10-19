namespace Application.Employees
{
    public record UpdateEmployeeByIdCommand : IRequest<Result<EmployeeDto>>
    {
        public EmployeeDto? EmployeeDto { get; init; }
    }

    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeByIdCommand>
    {
        public UpdateEmployeeValidator()
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

    internal class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeByIdCommand, Result<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<EmployeeDto>> Handle(UpdateEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeDto!.Id);

            if (employee is null)
                return Result.Fail<EmployeeDto>("Employee not found");

            employee.Firstname = request.EmployeeDto.Firstname;
            employee.Lastname = request.EmployeeDto.Lastname;
            employee.Email = request.EmployeeDto.Email;
            

            await _employeeRepository.Update(employee);
            await _unitOfWork.SaveCommitAsync();

            var response = employee.Adapt<EmployeeDto>();

            return Result.Ok(response);
        }
    }
}
