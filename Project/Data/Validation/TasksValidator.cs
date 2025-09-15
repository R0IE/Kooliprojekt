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
            RuleFor(x => x.InCharge).NotNull();
            RuleFor(x => x.Description).NotNull();
            RuleFor(x => x.WorkDone).NotNull();
            RuleFor(x => x.Files).NotNull();
        }
    }
}
