using System.Collections.Generic;
using System.Threading.Tasks;
using EOrchestralBriefcase.Application.Dtos.OrchestralBriefcases;

namespace EOrchestralBriefcase.Application.Interfaces
{
    public interface IOrchestralBriefcasesService
    {
        Task<List<OrchestralBriefcaseReadDto>> GetAllAsync();
        Task<OrchestralBriefcaseReadDto> GetByIdAsync(int id);
        Task<int> CreateAsync(OrchestralBriefcaseCreateDto createDto);
        Task UpdateAsync(OrchestralBriefcaseUpdateDto updateDto);
        Task DeleteByIdAsync(int id);
    }
}
