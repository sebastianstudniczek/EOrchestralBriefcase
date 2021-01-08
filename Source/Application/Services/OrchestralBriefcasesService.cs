using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EOrchestralBriefcase.Application.Dtos.OrchestralBriefcases;
using EOrchestralBriefcase.Application.Exceptions;
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

        public Task<List<OrchestralBriefcaseReadDto>> GetAllAsync()
        {
            return _dbContext.OrchestralBriefcases
                .ProjectTo<OrchestralBriefcaseReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<OrchestralBriefcaseReadDto> GetByIdAsync(int id)
        {
            var entity = await _dbContext.OrchestralBriefcases
                .FindAsync(id)
                .ConfigureAwait(false);

            if (entity is null)
            {
                throw new NotFoundException(nameof(OrchestralBriefcase), id);
            }

            return _mapper.Map<OrchestralBriefcaseReadDto>(entity);
        }

        public async Task<int> CreateAsync(OrchestralBriefcaseCreateDto createDto)
        {
            var entity = new OrchestralBriefcase { Name = createDto.Name };

            _dbContext.OrchestralBriefcases.Add(entity);
            await _dbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);

            return entity.Id;
        }

        public async Task UpdateAsync(OrchestralBriefcaseUpdateDto updateDto)
        {
            var entity = await _dbContext.OrchestralBriefcases
                .FindAsync(updateDto.Id)
                .ConfigureAwait(false);

            if (entity is null)
            {
                throw new NotFoundException(nameof(OrchestralBriefcase), updateDto.Id);
            }

            entity.Name = updateDto.Name;

            await _dbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _dbContext.OrchestralBriefcases
                .FindAsync(id)
                .ConfigureAwait(false);

            if (entity is null)
            {
                throw new NotFoundException(nameof(OrchestralBriefcase), id);
            }

            _dbContext.OrchestralBriefcases.Remove(entity);
            await _dbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }
    }
}
