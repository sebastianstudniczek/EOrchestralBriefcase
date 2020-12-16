using EOrchestralBriefcase.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EOrchestralBriefcase.BlazorUI.ViewModels.OrchestralPiece
{
    public interface IOrchestralPieceViewModel
    {
        string OrchestralBriefcaseName { get; set; }
        OrchestralPieceVm OrchestralPiece { get; set; }
        IEnumerable<OrchestralPieceVm> OrchestralPieces { get; set; }

        Task GetByIdAsync(int id);
        Task GetAllForBriefcase(int orchBriefcaseId);
        Task CreateAsync();
        Task UpdateAsync();
        Task DeleteAsync(int id);
    }
}