using FluentValidation;

namespace KooliProjekt.Data.Validation
{
    public class WorkLogsValidator : AbstractValidator<WorkLogs>
    {
        public WorkLogsValidator() 
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.TimeCost).NotEmpty();
            RuleFor(x => x.Performer).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
