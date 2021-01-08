using System.Collections.Generic;
using System.Threading.Tasks;
using EOrchestralBriefcase.Application.Dtos.OrchestralBriefcases;
using EOrchestralBriefcase.BlazorUI.Services;
using Microsoft.AspNetCore.Components;

namespace EOrchestralBriefcase.BlazorUI.Shared
{
    public partial class NavMenu
    {
        private bool _collapseNavMenu;

        [Inject]
        public IOrchestralBriefcasesService OrchestralBriefcasesService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public IEnumerable<OrchestralBriefcaseReadDto> OrchestralBriefcases { get; set; }
        public bool CollapseNavMenu { get; set; }
        public string NavMenuCssClass
        {
            get { return _collapseNavMenu ? "collapse" : null; }
        }
        public void ToggleNavMenu()
        {
            _collapseNavMenu = !_collapseNavMenu;
        }

        protected override async Task OnInitializedAsync()
        {
            OrchestralBriefcases = await OrchestralBriefcasesService.GetAllAsync();
        }

        protected void HandleClick(int id)
        {
            NavigationManager.NavigateTo($"orchestralbriefcases/{id}", true);
        }
    }
}
