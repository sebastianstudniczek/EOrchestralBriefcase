using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EOrchestralBriefcase.Application.Dtos;
using EOrchestralBriefcase.Application.Interfaces;
using EOrchestralBriefcase.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EOrchestralBriefcase.Application.Services
{
    public class OrchestralBriefcasesService : IOrchestralBriefcasesService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrchestralBriefcasesService(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OrchestralBriefcaseDto> GetByIdAsync(int id)
        {
            var orchBriefcaseDto = await _dbContext.OrchestralBriefcases
                .Where(orchBriefcase => orchBriefcase.Id == id)
                .ProjectTo<OrchestralBriefcaseDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            var orchPiecesForBriefcase = await GetAllOrchestralPiecesForBriefcase(id);

            if (orchBriefcaseDto != null)
            {
                orchBriefcaseDto.OrchestralPieces = orchPiecesForBriefcase;
            }

            return orchBriefcaseDto;
        }

        public async Task<List<OrchestralPieceDto>> GetAllOrchestralPiecesForBriefcase(int orchBriefcaseId)
        {
            var orchPieces = await _dbContext.OrchestralPieces
                .Where(orchPiece => orchPiece.OrchestralBriefcaseLinks
                    .Any(link => link.OrchestralBriefcaseId == orchBriefcaseId))
                .ProjectTo<OrchestralPieceDto>(_mapper.ConfigurationProvider)
                .OrderBy(orchPiece => orchPiece.NumberInBriefcase)
                .ToListAsync();

            return orchPieces;
        }

        public async Task<List<OrchestralBriefcaseDto>> GetAllAsync()
        {
            var orchBriefcasesDto = await _dbContext.OrchestralBriefcases
                .ProjectTo<OrchestralBriefcaseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return orchBriefcasesDto;
        }

        public async Task<int> InsertAsync(OrchestralBriefcaseDto dto)
        {
            var orchBriefcase = _mapper.Map<OrchestralBriefcase>(dto);
            _dbContext.OrchestralBriefcases.Add(orchBriefcase);

            await _dbContext.SaveChangesAsync();
            return orchBriefcase.Id;
        }

        public async Task UpdateAsync(OrchestralBriefcaseDto dto)
        {
            var orchestralBriefcase = _mapper.Map<OrchestralBriefcase>(dto);
            _dbContext.OrchestralBriefcases.Update(orchestralBriefcase);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var orchestralBriefcase = await _dbContext.OrchestralBriefcases
                .FirstOrDefaultAsync(orchBriefcase => orchBriefcase.Id == id);

            if (orchestralBriefcase != null)
            {
                _dbContext.OrchestralBriefcases.Remove(orchestralBriefcase);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
