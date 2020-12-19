using EOrchestralBriefcase.Application.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EOrchestralBriefcase.BlazorUI.ViewModels.OrchestralBriefcase
{
    public class OrchestralBriefcaseViewModel : IOrchestralBriefcaseViewModel ,INotifyPropertyChanged
    {
        private readonly HttpClient _apiClient;
        private static int _apiVersion = 1;
        private readonly string _baseWebApiCall = $"api/v{_apiVersion}/orchestralbriefcases";

        public OrchestralBriefcaseViewModel(HttpClient httpClient)
        {
            _apiClient = httpClient;
            OrchestralBriefcase = new OrchestralBriefcaseVm();
        }
        public string OrchestralBriefcaseName { get; set; }
        private IEnumerable<OrchestralBriefcaseVm> _orchestralBriefcases;
        public IEnumerable<OrchestralBriefcaseVm> OrchestralBriefcases
        {
            get => _orchestralBriefcases;
            set
            {
                _orchestralBriefcases = value;
                OnPropertyChanged();
            }
        }

        public OrchestralBriefcaseVm OrchestralBriefcase { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public async Task GetAllAsync()
        {
            var orchBriefcaseVm =
                await _apiClient.GetFromJsonAsync<List<OrchestralBriefcaseVm>>($"{_baseWebApiCall}");

            OrchestralBriefcases = orchBriefcaseVm.ToList();
        }

        public async Task GetByIdAsync(int id)
        {
            OrchestralBriefcase =
                await _apiClient.GetFromJsonAsync<OrchestralBriefcaseVm>($"{_baseWebApiCall}/{id}");
            OrchestralBriefcaseName = OrchestralBriefcase.Name;
        }

        public async Task CreateAsync()
        {
            await _apiClient.PostAsJsonAsync($"{_baseWebApiCall}", OrchestralBriefcase);
        }

        public async Task UpdateAsync()
        {
            await _apiClient.PutAsJsonAsync($"{_baseWebApiCall}/{OrchestralBriefcase.Id}", OrchestralBriefcase);
        }

        public async Task DeleteAsync(int id)
        {
            await _apiClient.DeleteAsync($"{_baseWebApiCall}/{id}");
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
