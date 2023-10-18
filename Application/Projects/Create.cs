using Application.Contracts;

namespace Application.Projects
{
    public record CreateProjectCommand : IRequest<ProjectDto>
    {
        public ProjectDto? ProjectDto { get; init; }
    }

    public class CreateProjectValidetor : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectValidetor()
        {
            RuleFor(c => c.ProjectDto).NotNull();
            When(c => c.ProjectDto != null,
                () =>
                {
                    RuleFor(c => c.ProjectDto!.Name).NotEmpty().Length(1, 150);
                    RuleFor(c => c.ProjectDto!.Description).Length(2000);
                    RuleFor(c => c.ProjectDto!.CustomerCompanyName).NotEmpty().Length(1, 200);
                });
        }
    }
}
