using System.Reflection;
using System.Threading.Tasks;
using EOrchestralBriefcase.Application.Interfaces;
using EOrchestralBriefcase.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EOrchestralBriefcase.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        public DbSet<OrchestralBriefcase> OrchestralBriefcases { get; set; }
        public DbSet<OrchestralPiece> OrchestralPieces { get; set; }
        public DbSet<SheetFile> SheetFiles { get; set; }
        public DbSet<OrchestralBriefcaseOrchestralPiece> OrchestralBriefcaseOrchestralPiece { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
