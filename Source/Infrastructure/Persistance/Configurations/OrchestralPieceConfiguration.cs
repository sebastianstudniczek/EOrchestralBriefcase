using EOrchestralBriefcase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EOrchestralBriefcase.Infrastructure.Persistance.Configurations
{
    public class OrchestralPieceConfiguration : IEntityTypeConfiguration<OrchestralPiece>
    {
        public void Configure(EntityTypeBuilder<OrchestralPiece> builder)
        {
            builder.Property(p => p.Id).HasColumnName("OrchestralPieceID");

            builder.Property(p => p.Title)
                .HasMaxLength(80)
                .IsUnicode()
                .IsRequired();

            builder.Property(p => p.Composer)
                .HasMaxLength(80)
                .IsUnicode();

            builder.Property(p => p.SongLink)
                .HasMaxLength(80)
                .IsUnicode(false);
        }
    }
}
