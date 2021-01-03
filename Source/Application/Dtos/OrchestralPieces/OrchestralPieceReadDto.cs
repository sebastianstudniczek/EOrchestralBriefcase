using System.Collections.Generic;
using EOrchestralBriefcase.Application.Mappings;
using EOrchestralBriefcase.Domain.Entities;

namespace EOrchestralBriefcase.Application.Dtos.OrchestralPieces
{
    public class OrchestralPieceReadDto : IMapFrom<OrchestralPiece>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Composer { get; set; }
        public int? Tempo { get; set; }
        public string SongLink { get; set; }
        public IList<OrchestralBriefcaseOrchestralPieceDto> OrchestralBriefcasesLinks { get; set; }
            = new List<OrchestralBriefcaseOrchestralPieceDto>();
    }
}
