using EOrchestralBriefcase.Application.Mappings;
using EOrchestralBriefcase.Domain.Entities;

namespace EOrchestralBriefcase.Application.Dtos.OrchestralBriefcases
{
    public class OrchestralBriefcaseReadDto : IMapFrom<OrchestralBriefcase>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
