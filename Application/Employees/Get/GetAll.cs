using Application.Contracts;

namespace Application.Employees.Get
{
    public record GetAllEmployeeRequest : IRequest<Result<IEnumerable<EmployeeDto>>> { }

    internal class GetAllEmployeeHandler : IRequestHandler<GetAllEmployeeRequest, Result<IEnumerable<EmployeeDto>>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ??
                throw new ArgumentNullException(nameof(employeeRepository)); ;
        }

        public async Task<Result<IEnumerable<EmployeeDto>>> Handle(GetAllEmployeeRequest request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllAsync();
            if (employees == null)
                return new Result<IEnumerable<EmployeeDto>>();
            return Result.Ok(employees.Select(employee => employee.Adapt<EmployeeDto>()));
        }
    }
}
