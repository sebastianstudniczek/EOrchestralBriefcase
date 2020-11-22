using System.Collections.Generic;

using Domain.Entities;

namespace EOrchestralBriefcase.Domain.Entities
{
    public class OrchestralPiece : BaseEntity
    {
        public string Title { get; set; }
        public string Composer { get; set; }
        public int? Tempo { get; set; }
        public string SongLink { get; set; }
        public ICollection<OrchestralBriefcaseOrchestralPiece> OrchestralBriefcaseLinks { get;}
            = new HashSet<OrchestralBriefcaseOrchestralPiece>();
        public ICollection<SheetFile> SheetFiles { get; } = new HashSet<SheetFile>();
    }
}
