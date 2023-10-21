namespace Application.Feature.Employees.Get.GetAll
{
    public record GetAllEmployeeRequest : IRequest<Result<IEnumerable<EmployeeProfileDto>>> { }

    public class GetAllEmployeeValidator : AbstractValidator<GetAllEmployeeRequest>
    {
        public GetAllEmployeeValidator() { }
    }

    public class GetAllEmployeeHandler : IRequestHandler<GetAllEmployeeRequest, Result<IEnumerable<EmployeeProfileDto>>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ??
                throw new ArgumentNullException(nameof(employeeRepository));
        }

        public async Task<Result<IEnumerable<EmployeeProfileDto>>> Handle(GetAllEmployeeRequest request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllAsync();
            if (employees == null)
                return new Result<IEnumerable<EmployeeProfileDto>>();
            return Result.Ok(employees.Select(employee => employee.Adapt<EmployeeProfileDto>()));
        }
    }
}
