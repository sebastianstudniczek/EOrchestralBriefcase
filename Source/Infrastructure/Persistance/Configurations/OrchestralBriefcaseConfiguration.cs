using EOrchestralBriefcase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EOrchestralBriefcase.Infrastructure.Persistance.Configurations
{
    public class OrchestralBriefcaseConfiguration : IEntityTypeConfiguration<OrchestralBriefcase>
    {
        public void Configure(EntityTypeBuilder<OrchestralBriefcase> builder)
        {
            builder.Property(p => p.Id).HasColumnName("OrchestralBriefcaseID");

            builder.Property(p => p.Name)
                .HasMaxLength(80)
                .IsUnicode()
                .IsRequired();
        }
    }
}
