using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EOrchestralBriefcase.Application.Dtos.OrchestralPieces;
using EOrchestralBriefcase.Application.Exceptions;
using EOrchestralBriefcase.Application.Interfaces;
using EOrchestralBriefcase.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
        public async Task<List<OrchestralPieceReadDto>> GetAllForOrchestralBriefcaseAsync(int orchestralBriefcaseId)
        {
            var orchestralBriefcase = await _dbContext.OrchestralBriefcases
                .FindAsync(orchestralBriefcaseId)
                .ConfigureAwait(false);

            if (orchestralBriefcase is null)
            {
                throw new NotFoundException(nameof(OrchestralBriefcase), orchestralBriefcaseId);
            }

            return await _dbContext.OrchestralPieces
                .Where(orchestralPiece => orchestralPiece.OrchestralBriefcaseLinks
                    .Any(link => link.OrchestralBriefcaseId == orchestralBriefcaseId))
                .ProjectTo<OrchestralPieceReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<OrchestralPieceReadDto> GetById(int id)
        {
            var entity = await _dbContext.OrchestralPieces
                .Include(entity => entity.OrchestralBriefcaseLinks)
                .FirstOrDefaultAsync(entity => entity.Id == id)
                .ConfigureAwait(false);

            if (entity is null)
            {
                throw new NotFoundException(nameof(OrchestralPiece), id);
            }

            return _mapper.Map<OrchestralPieceReadDto>(entity);
        }

        public async Task<int> CreateAsync(OrchestralPieceCreateDto createDto)
        {
            //foreach (var link in createDto.OrchestralBriefcasesLinks)
            //{
            //    bool doesOrchestralBriefcaseExist = await _dbContext.OrchestralBriefcases
            //        .AnyAsync(orchBriefcase => orchBriefcase.Id == link.OrchestralBriefcaseId)
            //        .ConfigureAwait(false);

            //    if (doesOrchestralBriefcaseExist is false)
            //    {
            //        throw new NotFoundException(nameof(OrchestralBriefcase), )
            //    }
            //}
            var orchestralPiece = new OrchestralPiece
            {
                Title = createDto.Title,
                Composer = createDto.Composer,
                Tempo = createDto.Tempo,
                SongLink = createDto.SongLink
            };

            foreach (var link in createDto.OrchestralBriefcasesLinks)
            {
                orchestralPiece.OrchestralBriefcaseLinks.Add(
                    new OrchestralBriefcaseOrchestralPiece
                    {
                        OrchestralPiece = orchestralPiece,
                        OrchestralBriefcaseId = link.OrchestralBriefcaseId,
                        NumberInOrchestralBriefcase = link.NumberInOrchestralBriefcase
                    });
            }

            _dbContext.OrchestralPieces.Add(orchestralPiece);
            await _dbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);

            return orchestralPiece.Id;
        }

        public async Task UpdateAsync(OrchestralPieceUpdateDto updateDto)
        {
            var entity = await _dbContext.OrchestralPieces
                .Include(entity => entity.OrchestralBriefcaseLinks)
                .FirstOrDefaultAsync(entity => entity.Id == updateDto.Id);

            if (entity is null)
            {
                throw new NotFoundException(nameof(OrchestralPiece), updateDto.Id);
            }

            entity.Title = updateDto.Title;
            entity.Composer = updateDto.Composer;
            entity.Tempo = updateDto.Tempo;
            entity.SongLink = updateDto.SongLink;

            foreach (var link in updateDto.OrchestralBriefcasesLinks)
            {
                entity.OrchestralBriefcaseLinks.Add(
                    new OrchestralBriefcaseOrchestralPiece
                    {
                        OrchestralPieceId = entity.Id,
                        OrchestralBriefcaseId = link.OrchestralBriefcaseId,
                        NumberInOrchestralBriefcase = link.NumberInOrchestralBriefcase
                    });
            }

            await _dbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _dbContext.OrchestralPieces
                .FirstOrDefaultAsync(entity => entity.Id == id)
                .ConfigureAwait(false);

            if (entity is null)
            {
                throw new NotFoundException(nameof(OrchestralPiece), id);

            }

            _dbContext.OrchestralPieces.Remove(entity);
            await _dbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }
    }
}
