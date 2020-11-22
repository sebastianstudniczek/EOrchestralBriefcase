using EOrchestralBriefcase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EOrchestralBriefcase.Infrastructure.Persistance.Configurations
{
    public class SheetFileConfiguration : IEntityTypeConfiguration<SheetFile>
    {
        public void Configure(EntityTypeBuilder<SheetFile> builder)
        {
            builder.Property(p => p.Id).HasColumnName("SheetFileID");

            builder.Property(p => p.Title)
                .HasMaxLength(80)
                .IsUnicode()
                .IsRequired();
        }
    }
}
