using EOrchestralBriefcase.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EOrchestralBriefcase.Application.Interfaces
{
    public interface IOrchestralBriefcasesService
    {
        Task<OrchestralBriefcaseDto> GetByIdAsync(int id);
        Task<List<OrchestralBriefcaseDto>> GetAllAsync();
        Task<int> InsertAsync(OrchestralBriefcaseDto dto);
        Task UpdateAsync(OrchestralBriefcaseDto dto);
        Task DeleteByIdAsync(int id);
    }
}
