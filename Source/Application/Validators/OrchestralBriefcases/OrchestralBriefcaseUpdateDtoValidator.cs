using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EOrchestralBriefcase.Application.Dtos.OrchestralBriefcases;
using EOrchestralBriefcase.Application.Interfaces;
using EOrchestralBriefcase.Application.Validators.Rules;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EOrchestralBriefcase.Application.Validators.OrchestralBriefcases
{
    public class OrchestralBriefcaseUpdateDtoValidator : AbstractValidator<OrchestralBriefcaseUpdateDto>
    {
        private readonly IApplicationDbContext _dbContext;

        public OrchestralBriefcaseUpdateDtoValidator(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(orchBriefcase => orchBriefcase.Name)
                .NameValidation()
                .MustAsync(BeUniqueName)
                    .WithMessage("Orchestral briefcase with the specified name already exists.");
        }

        private Task<bool> BeUniqueName(
            OrchestralBriefcaseUpdateDto updateDto,
            string name,
            CancellationToken cancellationToken)
        {
            return _dbContext.OrchestralBriefcases
                .Where(orchBriefcase => orchBriefcase.Id != updateDto.Id)
                .AllAsync(orchBriefcase => orchBriefcase.Name != updateDto.Name, cancellationToken);
        }
    }
}
