using System.Collections.Generic;

namespace EOrchestralBriefcase.Application.Dtos.OrchestralPieces
{
    public class OrchestralPieceCreateDto
    {
        public string Title { get; set; }
        public string Composer { get; set; }
        public int? Tempo { get; set; }
        public string SongLink { get; set; }
        public IList<OrchestralBriefcaseOrchestralPieceDto> OrchestralBriefcasesLinks { get; set; }
            = new List<OrchestralBriefcaseOrchestralPieceDto>();
    }
}
