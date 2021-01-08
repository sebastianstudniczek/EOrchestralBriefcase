using System.Collections.Generic;
using System.Threading.Tasks;
using EOrchestralBriefcase.Application.Dtos.OrchestralPieces;

namespace EOrchestralBriefcase.Application.Interfaces
{
    public interface IOrchestralPiecesService
    {
        Task<List<OrchestralPieceReadDto>> GetAllForOrchestralBriefcaseAsync(int orchestralBriefcaseId);
        Task<OrchestralPieceReadDto> GetById(int id);
        Task<int> CreateAsync(OrchestralPieceCreateDto createDto);
        Task UpdateAsync(OrchestralPieceUpdateDto updateDto);
        Task DeleteByIdAsync(int id);
    }
}
