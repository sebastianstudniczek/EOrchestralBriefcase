using EOrchestralBriefcase.Application.Mappings;
using EOrchestralBriefcase.Domain.Entities;

namespace EOrchestralBriefcase.Application.Dtos.OrchestralBriefcases
{
    public class OrchestralBriefcaseCreateDto : IMapFrom<OrchestralBriefcase>
    {
        public string Name { get; set; }
    }
}
