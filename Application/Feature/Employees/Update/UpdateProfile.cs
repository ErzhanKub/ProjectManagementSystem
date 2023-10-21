namespace Application.Feature.Employees.Update
{
    public record UpdateEmployeeByIdCommand : IRequest<Result<UEmployeeProfileDto>>
    {
        public UEmployeeProfileDto? EmployeeDto { get; init; }
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
                    RuleFor(c => c.EmployeeDto!.Patronymic).Length(1, 200);
                    RuleFor(c => c.EmployeeDto!.Email).NotEmpty().EmailAddress();
                });
        }
    }

    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeByIdCommand, Result<UEmployeeProfileDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<UEmployeeProfileDto>> Handle(UpdateEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeDto!.Id);

            if (employee is null)
                return Result.Fail<UEmployeeProfileDto>("Employee not found");

            employee = request.EmployeeDto.Adapt(employee);

            _employeeRepository.Update(employee);
            await _unitOfWork.SaveCommitAsync();

            return Result.Ok(request.EmployeeDto);
        }
    }
}
