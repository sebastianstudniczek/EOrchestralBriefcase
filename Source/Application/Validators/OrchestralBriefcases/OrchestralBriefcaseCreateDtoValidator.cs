using System.Threading;
using System.Threading.Tasks;
using EOrchestralBriefcase.Application.Dtos.OrchestralBriefcases;
using EOrchestralBriefcase.Application.Interfaces;
using EOrchestralBriefcase.Application.Validators.Rules;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EOrchestralBriefcase.Application.Validators.OrchestralBriefcases
{
    public class OrchestralBriefcaseCreateDtoValidator : AbstractValidator<OrchestralBriefcaseCreateDto>
    {
        private readonly IApplicationDbContext _dbContext;

        public OrchestralBriefcaseCreateDtoValidator(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(orchBriefcase => orchBriefcase.Name)
                .NameValidation()
                .MustAsync(BeUniqueName)
                    .WithMessage("Orchestral briefcase with the specified name already exists.");
        }

        private Task<bool> BeUniqueName(
            OrchestralBriefcaseCreateDto createDto,
            string name,
            CancellationToken cancellationToken)
        {
            return _dbContext.OrchestralBriefcases
                .AllAsync(orchBriefcase => orchBriefcase.Name != createDto.Name, cancellationToken);
        }

    }
}
