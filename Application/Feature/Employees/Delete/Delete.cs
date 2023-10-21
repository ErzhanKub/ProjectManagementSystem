namespace Application.Feature.Employees.Delete
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

    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeByIdCommand, Result<Guid[]>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository ??
                throw new ArgumentNullException(nameof(employeeRepository));
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid[]>> Handle(DeleteEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _employeeRepository.DeleteByIdAsync(request.Id!).ConfigureAwait(false);
            if (response != null)
            {
                await _unitOfWork.SaveCommitAsync();
                return Result.Ok(response);
            }

            return Result.Fail<Guid[]>("Employee not dound");
        }
    }
}
