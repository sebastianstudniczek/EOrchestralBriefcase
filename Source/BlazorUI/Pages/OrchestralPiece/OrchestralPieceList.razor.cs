using System.Collections.Generic;
using System.Threading.Tasks;
using EOrchestralBriefcase.Application.Dtos.OrchestralPieces;
using EOrchestralBriefcase.BlazorUI.Services;
using Microsoft.AspNetCore.Components;

namespace EOrchestralBriefcase.BlazorUI.Pages.OrchestralPiece
{
    public partial class OrchestralPieceList
    {
        [Inject]
        public IOrchestralBriefcasesService OrchestralBriefcasesService { get; set; }
        [Inject]
        public IOrchestralPiecesService OrchestralPiecesService { get; set; }

        [Parameter]
        public int Id { get; set; }

        public string OrchestralBriefcaseName { get; set; }
        public IEnumerable<OrchestralPieceReadDto> OrchestralPieces { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            OrchestralPieces = await OrchestralBriefcasesService
                .GetAllOrchestralPiecesForBriefcase(Id);
            var orchestralBriefcase = await OrchestralBriefcasesService
                .GetByIdAsync(Id);
            OrchestralBriefcaseName = orchestralBriefcase.Name;
        }

        protected async Task OnDeleteOrchestralPieceAsync(int orchestralPieceId)
        {
            await OrchestralPiecesService.DeleteOrchestralPieceById(orchestralPieceId);
            OrchestralPieces = await OrchestralBriefcasesService.GetAllOrchestralPiecesForBriefcase(Id);
        }
    }
}
