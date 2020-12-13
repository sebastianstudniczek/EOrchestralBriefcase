using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EOrchestralBriefcase.Application.Dtos;
using EOrchestralBriefcase.Application.Interfaces;
using EOrchestralBriefcase.Application.Mappings;
using EOrchestralBriefcase.Application.Services;
using EOrchestralBriefcase.Domain.Entities;
using EOrchestralBriefcase.Infrastructure.Persistance;
using EOrchestralBriefcase.Tests.Application.UnitTests.Helper;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EOrchestralBriefcase.Tests.Application.UnitTests
{
    public class OrchestralPieceServiceTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;
        public OrchestralPieceServiceTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = _configuration.CreateMapper();

            ContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "orchestralPieceServiceTests")
                .Options;

            DataSeeder.Seed(ContextOptions);
        }
        public DbContextOptions<ApplicationDbContext> ContextOptions { get; }

        [Fact]
        public void GetAllForBriefcaseAsync_ShouldReturnAllOrchestralPiecesDto()
        {
            List<OrchestralPieceDto> expectedDtos = new List<OrchestralPieceDto>
            {
                new OrchestralPieceDto
                {
                    Id = 1,
                    Title = "ABBA GOLD",
                    Composer = "Ron Sebregets",
                    SongLink = "https://www.youtube.com/watch?v=3P9NC4dB89Y",
                    NumberInBriefcase = 1,
                    OrchestralBriefcaseId = 1 },
                new OrchestralPieceDto
                {
                    Id = 2,
                    Title = "Adria",
                    Composer = "Josef Raha" ,
                    NumberInBriefcase = 2,
                    OrchestralBriefcaseId = 1
                },
                new OrchestralPieceDto
                {
                    Id = 3,
                    Title = "Alte Kameraden Swing",
                    NumberInBriefcase = 3,
                    OrchestralBriefcaseId = 1
                },
                new OrchestralPieceDto
                {
                    Id = 4,
                    Title = "Bohemian Rhapsody",
                    Composer = "Freddie Mercury",
                    SongLink = "https://www.youtube.com/watch?v=rhmDm42Eljs",
                    NumberInBriefcase = 4,
                    OrchestralBriefcaseId = 1
                },
            };

            using (var context = new ApplicationDbContext(ContextOptions))
            {
                IOrchestralPiecesService orchPieceService = new OrchestralPiecesService(context, _mapper);
                List<OrchestralPieceDto> actualDtos =
                    orchPieceService.GetAllForBriefcaseAsync(expectedDtos[0].OrchestralBriefcaseId).Result;

                Assert.True(actualDtos.Count == 4);
                Assert.True(CustomComparer<OrchestralPieceDto>.CheckIfPropertiesAreEqual(
                    expectedDtos[0], actualDtos[0]));

                Assert.True(CustomComparer<OrchestralPieceDto>.CheckIfPropertiesAreEqual(
                    expectedDtos[1], actualDtos[1]));

                Assert.True(CustomComparer<OrchestralPieceDto>.CheckIfPropertiesAreEqual(
                    expectedDtos[2], actualDtos[2]));

                Assert.True(CustomComparer<OrchestralPieceDto>.CheckIfPropertiesAreEqual(
                    expectedDtos[3], actualDtos[3]));
            }
        }

        [Fact]
        public void InsertAsync_ShouldInsertOrchestralPiece()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InsertOrchestralPiece")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var orchBriefcase = new OrchestralBriefcase
                {
                    Id = 1,
                    Name = "Teczka czerwona - Krzyżanowice"
                };

                context.OrchestralBriefcases.Add(orchBriefcase);
                context.SaveChanges();
            }

            var orchPieceDto = new OrchestralPieceDto
            {
                Id = 1,
                Title = "ABBA GOLD",
                Composer = "Ron Sebregets",
                Tempo = 120,
                SongLink = "https://www.youtube.com/watch?v=3P9NC4dB89Y",
                OrchestralBriefcaseId = 1,
                NumberInBriefcase = 1
            };

            using (var context = new ApplicationDbContext(options))
            {
                IOrchestralPiecesService orchPieceService = new OrchestralPiecesService(context, _mapper);
                orchPieceService.InsertAsync(orchPieceDto);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var orchPieces = context.OrchestralPieces
                    .Include(x => x.OrchestralBriefcaseLinks)
                    .ToList();
                var insertedOrchPiece = orchPieces.First();

                Assert.Single(orchPieces);
                Assert.Equal(orchPieceDto.Id, insertedOrchPiece.Id);
                Assert.Equal(orchPieceDto.Title, insertedOrchPiece.Title);
                Assert.Equal(orchPieceDto.Composer, insertedOrchPiece.Composer);
                Assert.Equal(orchPieceDto.Tempo, insertedOrchPiece.Tempo);
                Assert.Equal(orchPieceDto.SongLink, insertedOrchPiece.SongLink);
                Assert.Equal(orchPieceDto.OrchestralBriefcaseId,
                    insertedOrchPiece.OrchestralBriefcaseLinks.First().OrchestralBriefcaseId);

                Assert.Equal(orchPieceDto.OrchestralBriefcaseId,
                    insertedOrchPiece.OrchestralBriefcaseLinks.First().NumberInBriefcase);
            }
        }
    }
}
