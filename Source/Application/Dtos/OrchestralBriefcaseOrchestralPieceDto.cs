using EOrchestralBriefcase.Application.Mappings;
using EOrchestralBriefcase.Domain.Entities;

namespace EOrchestralBriefcase.Application.Dtos
{
    public class OrchestralBriefcaseOrchestralPieceDto : IMapFrom<OrchestralBriefcaseOrchestralPiece>
    {
        public int NumberInOrchestralBriefcase { get; set; }
        public int OrchestralBriefcaseId { get; set; }
        public int OrchestralPieceId { get; set; }
    }
}
