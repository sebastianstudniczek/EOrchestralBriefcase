using System.Threading.Tasks;
using AutoMapper;
using EOrchestralBriefcase.Application.Dtos;
using EOrchestralBriefcase.Application.Dtos.OrchestralPieces;
using EOrchestralBriefcase.Application.Services;
using EOrchestralBriefcase.Application.UnitTests.Common;
using EOrchestralBriefcase.Infrastructure.Persistance;
using Xunit;

namespace EOrchestralBriefcase.Tests.Application.UnitTests
{
    public class OrchestralPieceServiceTests : ServiceTest, IClassFixture<ServiceTestFixture>
    {
        private readonly IMapper _mapper;
        public OrchestralPieceServiceTests(ServiceTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetAllForBriefcaseAsync_ShouldReturnAllOrchestralPieceDtosForOrchestralBriefcase()
        {
            DataSeeder.Seed(DbContextOptions);

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var orchestralPieceService = new OrchestralPiecesService(context, _mapper);
                var actualDtos = await orchestralPieceService.GetAllForOrchestralBriefcaseAsync(1);

                Assert.Equal(DataSeeder.OrchestralPieceCount, actualDtos.Count);
            }
        }

        [Fact]
        public async Task InsertAsync_ShouldInsertOrchestralPiece()
        {
            var createDto = new OrchestralPieceCreateDto
            {
                Title = "ABBA GOLD",
                Composer = "Ron Sebregets",
                Tempo = 120,
                SongLink = "https://www.youtube.com/watch?v=3P9NC4dB89Y",
                OrchestralBriefcasesLinks =
                {
                    new OrchestralBriefcaseOrchestralPieceDto
                    {
                        NumberInOrchestralBriefcase = 1,
                        OrchestralBriefcaseId = 1
                    }
                }
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var orchestralPieceService = new OrchestralPiecesService(context, _mapper);
                await orchestralPieceService.CreateAsync(createDto);
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var orchestralPiece = context.OrchestralPieces.Find(1);

                Assert.Equal(createDto.Title, orchestralPiece.Title);
                Assert.Equal(createDto.Composer, orchestralPiece.Composer);
                Assert.Equal(createDto.Tempo, orchestralPiece.Tempo);
                Assert.Equal(createDto.SongLink, orchestralPiece.SongLink);
            }
        }
    }
}
