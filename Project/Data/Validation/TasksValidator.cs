using FluentValidation;

namespace KooliProjekt.Data.Validation
{
    public class TasksValidator : AbstractValidator<Tasks>
    {
        public TasksValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Title).NotNull();
            RuleFor(x => x.TaskStart).NotNull();
            RuleFor(x => x.ExpectedTime).NotNull();
            // ExpectedTime maps to SQL 'time' which must be less than 24 hours
            RuleFor(x => x.ExpectedTime).LessThan(TimeSpan.FromDays(1)).WithMessage("Expected time must be less than 24 hours.");
            RuleFor(x => x.InCharge).NotNull();
            RuleFor(x => x.Description).NotNull();
            RuleFor(x => x.WorkDone).NotNull();
        }
    }
}
