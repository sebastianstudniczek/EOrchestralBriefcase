using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EOrchestralBriefcase.Application.Dtos.OrchestralPieces;

namespace EOrchestralBriefcase.BlazorUI.Services
{
    public class OrchestralPiecesService : IOrchestralPiecesService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseEndpoint = "api/v1/orchestralpieces";
        public OrchestralPiecesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<OrchestralPieceReadDto> GetOrchestralPieceById(int id)
        {
            string endpoint = _baseEndpoint + $"/{id}";

            return _httpClient
                .GetFromJsonAsync<OrchestralPieceReadDto>(endpoint);
        }

        public Task CreateOrchestralPiece(OrchestralPieceCreateDto createdto)
        {
            return _httpClient
                .PostAsJsonAsync(_baseEndpoint, createdto);
        }

        public Task UpdateOrchestralPiece(int id, OrchestralPieceUpdateDto updateDto)
        {
            string endpoint = $"{_baseEndpoint}/{id}";

            return _httpClient
                .PutAsJsonAsync(endpoint, updateDto);
        }

        public Task DeleteOrchestralPieceById(int id)
        {
            string endpoint = $"{_baseEndpoint}/{id}";

            return _httpClient.DeleteAsync(endpoint);
        }
    }
}
