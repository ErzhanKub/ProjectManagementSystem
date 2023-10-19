namespace Application.Employees.Get
{
    public record GetOneEmployeeRequest : IRequest<Result<FullEmployeeDto>>
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

    public class GetOneEmployeeHandler : IRequestHandler<GetOneEmployeeRequest, Result<FullEmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetOneEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ??
                throw new ArgumentNullException(nameof(employeeRepository));
        }

        public async Task<Result<FullEmployeeDto>> Handle(GetOneEmployeeRequest request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id).ConfigureAwait(false);
            if (employee == null)
                return Result.Fail<FullEmployeeDto>("Employee not found.");
            var response = employee.Adapt<FullEmployeeDto>();
            return Result.Ok(response);
        }
    }
}
