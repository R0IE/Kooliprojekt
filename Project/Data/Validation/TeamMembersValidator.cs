using FluentValidation;

namespace KooliProjekt.Data.Validation
{
    public class TeamMembersValidator : AbstractValidator<TeamMembers>
    {
        public TeamMembersValidator() 
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.ProjectId).NotEmpty();
        }
    }
}
