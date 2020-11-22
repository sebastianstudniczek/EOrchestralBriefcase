using Domain.Entities;
using System.Collections.Generic;

namespace EOrchestralBriefcase.Domain.Entities
{
    public class OrchestralBriefcase : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<OrchestralBriefcaseOrchestralPiece> OrchestralPieceLinks { get; }
            = new HashSet<OrchestralBriefcaseOrchestralPiece>();
    }
}
