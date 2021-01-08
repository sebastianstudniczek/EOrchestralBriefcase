using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EOrchestralBriefcase.Application.Dtos;
using EOrchestralBriefcase.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EOrchestralBriefcase.Application.Validators
{
    public class OrchestralBriefcaseOrchestralPieceDtoValidator : AbstractValidator<OrchestralBriefcaseOrchestralPieceDto>
    {
        private readonly IApplicationDbContext _dbContext;

        public OrchestralBriefcaseOrchestralPieceDtoValidator(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(link => link.OrchestralBriefcaseId)
                .MustAsync(OrchestralBriefcaseExist)
                    .WithMessage((link, id) => $"Orchestral briefcase with the given id ({id}) doesn't exist.");

            RuleFor(link => link.NumberInOrchestralBriefcase)
                .MustAsync(NotBeTaken)
                    .WithMessage((link, number) =>
                        $"Specified number ({number}) in the orchestral briefcase is already taken.");

        }

        private Task<bool> OrchestralBriefcaseExist(
            int orchestralBriefcaseId,
            CancellationToken cancellationToken)
        {
            return _dbContext.OrchestralBriefcases
                .AnyAsync(orchBriefcase =>
                    orchBriefcase.Id == orchestralBriefcaseId, cancellationToken);
        }

        private Task<bool> NotBeTaken(
            int numberInBriefcase,
            CancellationToken cancellationToken)
        {
            return _dbContext.OrchestralBriefcaseOrchestralPiece
                .Where(link => link.OrchestralBriefcaseId == link.OrchestralBriefcaseId)
                .AnyAsync(link => link.NumberInOrchestralBriefcase != link.NumberInOrchestralBriefcase);
        }
    }
}
