using System.Collections.Generic;
using System.Linq;
using EOrchestralBriefcase.Application.Dtos;
using EOrchestralBriefcase.Application.Dtos.OrchestralPieces;
using EOrchestralBriefcase.Application.Interfaces;
using EOrchestralBriefcase.Application.Validators.Rules;
using FluentValidation;

namespace EOrchestralBriefcase.Application.Validators.OrchestralPieces
{
    public class OrchestralPieceCreateDtoValidator : AbstractValidator<OrchestralPieceCreateDto>
    {
        private readonly IApplicationDbContext _dbContext;

        public OrchestralPieceCreateDtoValidator(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(orchPiece => orchPiece.Title)
                .TitleValidation();

            RuleFor(orchPiece => orchPiece.Tempo)
                .TempoValidation();

            RuleFor(orchPiece => orchPiece.SongLink)
                .SongLinkValidation();

            RuleFor(orchPiece => orchPiece.OrchestralBriefcasesLinks)
                .Must(HaveDifferentOrchestralBriefcaseIds)
                    .WithMessage("Can't put one orchestral piece in the same orchestral briefcase multiple times.");

            RuleForEach(orchPiece => orchPiece.OrchestralBriefcasesLinks)
                .SetValidator(new OrchestralBriefcaseOrchestralPieceDtoValidator(_dbContext));
        }

        private bool HaveDifferentOrchestralBriefcaseIds(
            IList<OrchestralBriefcaseOrchestralPieceDto> links)
        {
            return links.Count == links.Select(x => x.OrchestralBriefcaseId).Distinct().Count();
        }
    }
}
