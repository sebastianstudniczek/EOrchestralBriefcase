using EOrchestralBriefcase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EOrchestralBriefcase.Infrastructure.Persistance.Configurations
{
    public class OrchestralBriefcaseOrchestralPieceConfiguration : IEntityTypeConfiguration<OrchestralBriefcaseOrchestralPiece>
    {
        public void Configure(EntityTypeBuilder<OrchestralBriefcaseOrchestralPiece> builder)
        {
            builder
                .HasKey(k => new { k.OrchestralBriefcaseId, k.OrchestralPieceId });

            builder.
                Property(p => p.NumberInBriefcase)
                .IsRequired();
        }
    }
}
