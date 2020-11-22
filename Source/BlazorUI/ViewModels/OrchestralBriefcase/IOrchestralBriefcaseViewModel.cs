using EOrchestralBriefcase.Application.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace EOrchestralBriefcase.BlazorUI.ViewModels.OrchestralBriefcase
{
    public interface IOrchestralBriefcaseViewModel
    {
        public string OrchestralBriefcaseName { get; set; }
        OrchestralBriefcaseVm OrchestralBriefcase { get; set; }
        IEnumerable<OrchestralBriefcaseVm> OrchestralBriefcases { get; set; }

        event PropertyChangedEventHandler PropertyChanged;

        Task GetByIdAsync(int id);
        Task GetAllAsync();
        Task CreateAsync();
        Task UpdateAsync();
        Task DeleteAsync(int id);
    }
}
