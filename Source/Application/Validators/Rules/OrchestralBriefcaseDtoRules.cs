using FluentValidation;

namespace EOrchestralBriefcase.Application.Validators.Rules
{
    public static class OrchestralBriefcaseDtoRules
    {
        public static IRuleBuilderOptions<T, string> NameValidation<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(80);
        }
    }
}
