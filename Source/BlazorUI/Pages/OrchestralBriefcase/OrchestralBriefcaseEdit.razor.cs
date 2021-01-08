using System.Threading.Tasks;
using EOrchestralBriefcase.Application.Dtos.OrchestralBriefcases;
using EOrchestralBriefcase.BlazorUI.Services;
using Microsoft.AspNetCore.Components;

namespace EOrchestralBriefcase.BlazorUI.Pages.OrchestralBriefcase
{
    public partial class OrchestralBriefcaseEdit
    {
        [Inject]
        public IOrchestralBriefcasesService OrchestralBriefcasesService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int Id { get; set; }
        public string PageHeaderText { get; set; }
        public string SubmitButtonText { get; set; }
        public string Message { get; set; }
        public OrchestralBriefcaseUpdateDto UpdateDto { get; set; }
            = new OrchestralBriefcaseUpdateDto();
        public OrchestralBriefcaseCreateDto CreateDto { get; set; }
            = new OrchestralBriefcaseCreateDto();

        protected override async Task OnParametersSetAsync()
        {
            if (Id != 0)
            {
                PageHeaderText = "Edytuj teczkę";
                SubmitButtonText = "Zapisz";
                var readDto = await OrchestralBriefcasesService.GetByIdAsync(Id);

                UpdateDto.Id = readDto.Id;
                UpdateDto.Name = readDto.Name;
            }
            else
            {
                PageHeaderText = "Dodaj nową teczkę";
                SubmitButtonText = "Dodaj";
            }
        }

        private async Task HandleValidSubmit()
        {
            if (Id != 0)
            {
                await OrchestralBriefcasesService
                    .UpdateAsync(Id, UpdateDto);
                Message = "Teczka została zaktualizowana.";
            }
            else
            {
                await OrchestralBriefcasesService
                    .CreateAsync(CreateDto);
                Message = "Teczka została dodana.";
            }
        }

        private void HandleAbort()
        {
            NavigationManager.NavigateTo($"orchestralbriefcases/{Id}");
        }
    }
}
