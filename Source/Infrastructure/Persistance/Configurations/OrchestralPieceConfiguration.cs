using EOrchestralBriefcase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EOrchestralBriefcase.Infrastructure.Persistance.Configurations
{
    public class OrchestralPieceConfiguration : IEntityTypeConfiguration<OrchestralPiece>
    {
        public void Configure(EntityTypeBuilder<OrchestralPiece> builder)
        {
            builder
                .Property(property => property.Id)
                .HasColumnName("OrchestralPieceID");

            builder
                .Property(property => property.Title)
                .HasMaxLength(80)
                .IsUnicode()
                .IsRequired();

            builder
                .Property(property => property.Composer)
                .HasMaxLength(80)
                .IsUnicode();

            builder
                .Property(property => property.SongLink)
                .HasMaxLength(80)
                .IsUnicode(false);
        }
    }
}
