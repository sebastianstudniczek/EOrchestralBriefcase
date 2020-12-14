using AutoMapper;
using AutoMapper.QueryableExtensions;
using EOrchestralBriefcase.Application.Dtos;
using EOrchestralBriefcase.Application.Interfaces;
using EOrchestralBriefcase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EOrchestralBriefcase.Application.Services
{
    public class OrchestralPiecesService : IOrchestralPiecesService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrchestralPiecesService(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OrchestralPieceDto> GetByIdAsync(int id)
        {
            var entity = await _dbContext.OrchestralPieces
                .FindAsync(id)
                .ConfigureAwait(false);

            return _mapper.Map<OrchestralPieceDto>(entity);
        }

        public Task<List<OrchestralPieceDto>> GetAllForBriefcaseAsync(int orchBriefcaseId)
        {
            return _dbContext.OrchestralPieces
                .Where(orchPiece => orchPiece.OrchestralBriefcaseLinks
                    .Any(orchBriefcaseLinks => orchBriefcaseLinks.OrchestralBriefcaseId == orchBriefcaseId))
                .ProjectTo<OrchestralPieceDto>(_mapper.ConfigurationProvider)
                .OrderBy(orchPieceDto => orchPieceDto.NumberInBriefcase)
                .ToListAsync();
        }

        public async Task<int> InsertAsync(OrchestralPieceDto orchPieceDto)
        {
            var orchPiece = _mapper.Map<OrchestralPiece>(orchPieceDto);
            var orchBriefcase = await _dbContext.OrchestralBriefcases
                .FirstOrDefaultAsync(oB => oB.Id == orchPieceDto.OrchestralBriefcaseId);

            orchPiece.OrchestralBriefcaseLinks.Add(
            new OrchestralBriefcaseOrchestralPiece
            {
                NumberInBriefcase = orchPieceDto.NumberInBriefcase,
                OrchestralBriefcase = orchBriefcase,
                OrchestralPiece = orchPiece
            });

            _dbContext.OrchestralPieces.Add(orchPiece);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return orchPiece.Id;
        }

        public async Task UpdateAsync(OrchestralPieceDto orchPieceDto)
        {
            var orchPiece = _mapper.Map<OrchestralPiece>(orchPieceDto);
            var orchBriefcase = await _dbContext.OrchestralBriefcases
                .FirstOrDefaultAsync(oB => oB.Id == orchPieceDto.OrchestralBriefcaseId)
                .ConfigureAwait(false);

            var orchBriefcaseLink = await _dbContext.OrchestralBriefcaseOrchestralPiece
                .FirstOrDefaultAsync(link =>
                    link.OrchestralBriefcaseId == orchPieceDto.OrchestralBriefcaseId
                    && link.OrchestralPieceId == orchPieceDto.Id)
                .ConfigureAwait(false);

            if (orchBriefcaseLink.NumberInBriefcase != orchPieceDto.NumberInBriefcase)
            {
                orchBriefcaseLink.NumberInBriefcase = orchPieceDto.NumberInBriefcase;
                _dbContext.OrchestralBriefcaseOrchestralPiece.Update(orchBriefcaseLink);
            }
            _dbContext.OrchestralPieces.Update(orchPiece);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
        {
            var orchPiece = await _dbContext.OrchestralPieces
                .FirstOrDefaultAsync(oP => oP.Id == id)
                .ConfigureAwait(false);

            if (orchPiece != null)
            {
                _dbContext.OrchestralPieces.Remove(orchPiece);
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
