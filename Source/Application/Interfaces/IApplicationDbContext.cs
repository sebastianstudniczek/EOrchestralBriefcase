using EOrchestralBriefcase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EOrchestralBriefcase.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<OrchestralBriefcase> OrchestralBriefcases { get; set; }
        DbSet<OrchestralPiece> OrchestralPieces { get; set; }
        DbSet<SheetFile> SheetFiles { get; set; }
        DbSet<OrchestralBriefcaseOrchestralPiece> OrchestralBriefcaseOrchestralPiece { get; set; }

        Task<int> SaveChangesAsync();
    }
}
