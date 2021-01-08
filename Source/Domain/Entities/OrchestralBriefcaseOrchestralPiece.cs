namespace EOrchestralBriefcase.Domain.Entities
{
    public class OrchestralBriefcaseOrchestralPiece
    {
        public int OrchestralBriefcaseId { get; set; }
        public OrchestralBriefcase OrchestralBriefcase { get; set; }
        public int OrchestralPieceId { get; set; }
        public OrchestralPiece OrchestralPiece { get; set; }

        public int NumberInOrchestralBriefcase { get; set; }
    }
}
