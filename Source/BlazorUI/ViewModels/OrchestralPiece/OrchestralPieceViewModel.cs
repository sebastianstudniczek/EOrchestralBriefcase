using EOrchestralBriefcase.Application.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EOrchestralBriefcase.BlazorUI.ViewModels.OrchestralPiece
{
    public class OrchestralPieceViewModel : IOrchestralPieceViewModel
    {
        private readonly HttpClient _apiClient;
        private static int _apiVersion = 1;
        private readonly string _baseWebApiCall = $"api/v{_apiVersion}/orchestralpieces";

        public OrchestralPieceViewModel(HttpClient httpClient)
        {
            _apiClient = httpClient;
            OrchestralPiece = new OrchestralPieceVm();
        }
        public string OrchestralBriefcaseName { get; set; }
        public IEnumerable<OrchestralPieceVm> OrchestralPieces { get; set; }
        public OrchestralPieceVm OrchestralPiece { get; set; }
        private OrchestralBriefcaseVm _orchestralBriefcase;

        public async Task GetByIdAsync(int id)
        {
            OrchestralPiece = await _apiClient.GetFromJsonAsync<OrchestralPieceVm>($"{_baseWebApiCall}/{id}");
        }
        public async Task GetAllForBriefcase(int orchBriefcaseId)
        {
            _orchestralBriefcase =
                await _apiClient.GetFromJsonAsync<OrchestralBriefcaseVm>($"api/v1/orchestralbriefcases/{orchBriefcaseId}");
            OrchestralPieces = _orchestralBriefcase.OrchestralPieces.ToList();
            OrchestralBriefcaseName = _orchestralBriefcase.Name;
        }

        public async Task CreateAsync()
        {
            await _apiClient.PostAsJsonAsync($"{_baseWebApiCall}", OrchestralPiece);
        }

        public async Task UpdateAsync()
        {
            await _apiClient.PutAsJsonAsync($"{_baseWebApiCall}/{OrchestralPiece.Id}", OrchestralPiece);
        }

        public async Task DeleteAsync(int id)
        {
            await _apiClient.DeleteAsync($"{_baseWebApiCall}/{id}");
        }
    }
}
