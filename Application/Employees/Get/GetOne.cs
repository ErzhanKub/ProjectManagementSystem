namespace Application.Employees.Get
{
    public record GetOneEmployeeRequest : IRequest<Result<EmployeeDto>>
    {
        public Guid Id { get; init; }
    }

    public class GetOneEmployeeValidator : AbstractValidator<GetOneEmployeeRequest>
    {
        public GetOneEmployeeValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }

    public class GetOneEmployeeHandler : IRequestHandler<GetOneEmployeeRequest, Result<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetOneEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ??
                throw new ArgumentNullException(nameof(employeeRepository));
        }

        public async Task<Result<EmployeeDto>> Handle(GetOneEmployeeRequest request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id).ConfigureAwait(false);
            if (employee == null)
                return Result.Fail<EmployeeDto>("Employee not found.");
            var response = employee.Adapt<EmployeeDto>();
            return Result.Ok(response);
        }
    }
}
