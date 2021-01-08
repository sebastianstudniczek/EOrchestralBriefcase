using System.Threading.Tasks;
using EOrchestralBriefcase.Application.Dtos.OrchestralPieces;

namespace EOrchestralBriefcase.BlazorUI.Services
{
    public interface IOrchestralPiecesService
    {
        Task<OrchestralPieceReadDto> GetOrchestralPieceById(int id);
        Task CreateOrchestralPiece(OrchestralPieceCreateDto createdto);
        Task UpdateOrchestralPiece(int id, OrchestralPieceUpdateDto updateDto);
        Task DeleteOrchestralPieceById(int id);
    }
}
