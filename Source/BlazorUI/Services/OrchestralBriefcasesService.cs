using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EOrchestralBriefcase.Application.Dtos.OrchestralBriefcases;
using EOrchestralBriefcase.Application.Dtos.OrchestralPieces;

namespace EOrchestralBriefcase.BlazorUI.Services
{
    public class OrchestralBriefcasesService : IOrchestralBriefcasesService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseEndpoint = "api/v1/orchestralbriefcases";
        public OrchestralBriefcasesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<IEnumerable<OrchestralBriefcaseReadDto>> GetAllAsync()
        {
            return _httpClient
                .GetFromJsonAsync<IEnumerable<OrchestralBriefcaseReadDto>>(_baseEndpoint);
        }

        public Task<OrchestralBriefcaseReadDto> GetByIdAsync(int id)
        {
            string endpoint = $"{_baseEndpoint}/{id}";

            return _httpClient
                .GetFromJsonAsync<OrchestralBriefcaseReadDto>(endpoint);
        }

        public Task CreateAsync(OrchestralBriefcaseCreateDto createDto)
        {
            return _httpClient
                .PostAsJsonAsync(_baseEndpoint, createDto);
        }

        public Task UpdateAsync(int id, OrchestralBriefcaseUpdateDto updateDto)
        {
            string endpoint = $"{_baseEndpoint}/{id}";

            return _httpClient.PutAsJsonAsync(endpoint, updateDto);
        }

        public Task DeleteByIdAsync(int id)
        {
            string endpoint = $"{_baseEndpoint}/{id}";

            return _httpClient.DeleteAsync(endpoint);
        }

        public Task<IEnumerable<OrchestralPieceReadDto>> GetAllOrchestralPiecesForBriefcase(int id)
        {
            string endpoint = $"{_baseEndpoint}/{id}/orchestralpieces";

            return _httpClient
                .GetFromJsonAsync<IEnumerable<OrchestralPieceReadDto>>(endpoint);
        }
    }
}
