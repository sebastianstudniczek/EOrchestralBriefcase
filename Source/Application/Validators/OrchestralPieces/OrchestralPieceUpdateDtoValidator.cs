using EOrchestralBriefcase.Application.Dtos.OrchestralPieces;
using EOrchestralBriefcase.Application.Validators.Rules;
using FluentValidation;

namespace EOrchestralBriefcase.Application.Validators.OrchestralPieces
{
    public class OrchestralPieceUpdateDtoValidator : AbstractValidator<OrchestralPieceUpdateDto>
    {
        public OrchestralPieceUpdateDtoValidator()
        {
            RuleFor(orchPiece => orchPiece.Title)
                .TitleValidation();

            RuleFor(orchPiece => orchPiece.Tempo)
                .TempoValidation();
        }
    }
}
