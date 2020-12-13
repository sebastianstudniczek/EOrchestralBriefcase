using EOrchestralBriefcase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EOrchestralBriefcase.Infrastructure.Persistance.Configurations
{
    public class SheetFileConfiguration : IEntityTypeConfiguration<SheetFile>
    {
        public void Configure(EntityTypeBuilder<SheetFile> builder)
        {
            builder
                .Property(property => property.Id)
                .HasColumnName("SheetFileID");

            builder
                .Property(property => property.Title)
                .HasMaxLength(80)
                .IsUnicode()
                .IsRequired();
        }
    }
}
