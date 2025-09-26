using FluentValidation;

namespace KooliProjekt.Data.Validation
{
    public class ProjectValidator : AbstractValidator<Project>
    {
        public ProjectValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.ProjectName).NotEmpty();
            RuleFor(x => x.Start).NotEmpty();
            RuleFor(x => x.Deadline).NotEmpty();
            RuleFor(x => x.Budget).NotEmpty();
            RuleFor(x => x.HourlyRate).NotEmpty();
            // Team is optional
            RuleFor(x => x.Team).MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Team));
        }
    }
}