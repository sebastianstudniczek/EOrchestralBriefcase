
using EOrchestralBriefcase.Application.ViewModels;

using FluentValidation;

namespace EOrchestralBriefcase.Application.Validators
{
    public class OrchestralBriefcaseVmValidator : AbstractValidator<OrchestralBriefcaseVm>
    {
        public OrchestralBriefcaseVmValidator()
        {
            RuleFor(orchBriefcase => orchBriefcase.Name)
                .NotEmpty()
                    .WithMessage("Nazwa jest wymagana")
                .MaximumLength(50)
                    .WithMessage("Nazwa nie powinna być dłuższa niż 50 znaków")
                .MinimumLength(2)
                    .WithMessage("Nazwa powinna być dłuższa niż 2 znaki");
        }
    }
}
