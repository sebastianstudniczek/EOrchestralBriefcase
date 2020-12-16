using EOrchestralBriefcase.Application.ViewModels;
using FluentValidation;
using static EOrchestralBriefcase.Application.Validators.FluentValidationHelper;

namespace EOrchestralBriefcase.Application.Validators
{
    public class OrchestralPieceVmValidator : AbstractValidator<OrchestralPieceVm>
    {
        public OrchestralPieceVmValidator()
        {
            RuleFor(orchPiece => orchPiece.Title)
                .NotEmpty()
                    .WithMessage("Tytuł jest wymagany")
                .MinimumLength(2)
                    .WithMessage("Tytuł powinien mieć przynajmniej 2 znaki")
                .MaximumLength(50)
                    .WithMessage("Tytuł nie powinien być dłuższy niż 50 znaków");

            RuleFor(orchPiece => orchPiece.Tempo)
                .GreaterThan(0)
                    .WithMessage("Wartość musi być większa od 0");

            RuleFor(orchPiece => orchPiece.SongLink)
                .Must(BeYoutubeUrl)
                    .WithMessage("Niewłaściwy link");

            RuleFor(orchPiece => orchPiece.NumberInBriefcase)
                .NotNull()
                    .WithMessage("Numer w teczce jest wymagany")
                .GreaterThan(0)
                    .WithMessage("Wartość musi być większa od 0")
                .Must(BeInteger)
                    .WithMessage("Wartość musi być liczbą całkowitą");

        }
    }
}
