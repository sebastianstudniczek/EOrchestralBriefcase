using System.Collections.Generic;
using System.Threading.Tasks;
using EOrchestralBriefcase.Application.Dtos.OrchestralBriefcases;
using EOrchestralBriefcase.Application.Dtos.OrchestralPieces;

namespace EOrchestralBriefcase.BlazorUI.Services
{
    public interface IOrchestralBriefcasesService
    {
        Task CreateAsync(OrchestralBriefcaseCreateDto createDto);
        Task DeleteByIdAsync(int id);
        Task<IEnumerable<OrchestralBriefcaseReadDto>> GetAllAsync();
        Task<IEnumerable<OrchestralPieceReadDto>> GetAllOrchestralPiecesForBriefcase(int id);
        Task<OrchestralBriefcaseReadDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, OrchestralBriefcaseUpdateDto updateDto);
    }
}