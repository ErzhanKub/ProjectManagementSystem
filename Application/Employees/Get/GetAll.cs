using Application.Contracts;

namespace Application.Employees.Get
{
    public record GetAllEmployeeRequest : IRequest<IEnumerable<EmployeeDto>> { }

    internal class GetAllEmployeeHandler : IRequestHandler<GetAllEmployeeRequest, IEnumerable<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository)); ;
        }

        public async Task<IEnumerable<EmployeeDto>> Handle(GetAllEmployeeRequest request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees.Select(employee => employee.Adapt<EmployeeDto>());
        }
    }
}
