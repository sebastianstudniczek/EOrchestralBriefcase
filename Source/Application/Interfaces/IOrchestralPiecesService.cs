using EOrchestralBriefcase.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EOrchestralBriefcase.Application.Interfaces
{
    public interface IOrchestralPiecesService
    {
        Task<OrchestralPieceDto> GetByIdAsync(int id, int orchBriefcaseId);
        Task<List<OrchestralPieceDto>> GetAllForBriefcaseAsync(int orchBriefcaseId);
        Task<int> InsertAsync(OrchestralPieceDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(OrchestralPieceDto dto);
    }
}
