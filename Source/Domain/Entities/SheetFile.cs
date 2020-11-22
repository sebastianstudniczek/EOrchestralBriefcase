using Domain.Entities;

namespace EOrchestralBriefcase.Domain.Entities
{
    public class SheetFile : BaseEntity
    {
        public string Title { get; set; }
        public byte[] FileData { get; set; }
        public int OrchestralPieceId { get; set; }
        public OrchestralPiece OrchestralPiece { get; set; }
    }
}
