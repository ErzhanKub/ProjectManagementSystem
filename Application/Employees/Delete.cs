namespace Application.Employees
{
    public record DeleteEmployeeByIdCommand : IRequest<Result<Guid[]>>
    {
        public Guid[]? Id { get; init; }
    }

    public class DeleteEmployeeValidator : AbstractValidator<DeleteEmployeeByIdCommand>
    {
        public DeleteEmployeeValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }

    internal class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeByIdCommand, Result<Guid[]>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ??
                throw new ArgumentNullException(nameof(employeeRepository));
        }

        public async Task<Result<Guid[]>> Handle(DeleteEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _employeeRepository.DeleteByIdAsync(request.Id!).ConfigureAwait(false);
            if (response != null)
                return Result.Ok(response);
            return Result.Fail<Guid[]>("Employee not dound");
        }
    }
}
