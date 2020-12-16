using System.Collections.Generic;
using AutoMapper;
using EOrchestralBriefcase.Application.Dtos;
using EOrchestralBriefcase.Application.Interfaces;
using EOrchestralBriefcase.Application.Mappings;
using EOrchestralBriefcase.Application.Services;
using EOrchestralBriefcase.Infrastructure.Persistance;
using EOrchestralBriefcase.Tests.Application.UnitTests.Helper;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EOrchestralBriefcase.Tests.Application.UnitTests
{
    public class OrchestralBriefcaseServiceTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;
        public OrchestralBriefcaseServiceTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = _configuration.CreateMapper();

            ContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "orchestralBriefcaseServiceTests")
                .Options;

            DataSeeder.Seed(ContextOptions);
        }
        public DbContextOptions<ApplicationDbContext> ContextOptions { get; }

        [Fact]
        public void GetAllAsync_ShouldReturnAllOrchestralBriefcaseDtos()
        {
            List<OrchestralBriefcaseDto> expectedOrchBriefcasesDto = new List<OrchestralBriefcaseDto>
            {
                new OrchestralBriefcaseDto { Id = 1, Name = "Czerwona" },
                new OrchestralBriefcaseDto { Id = 2, Name = "Czarna" },
                new OrchestralBriefcaseDto { Id = 3, Name = "Niebieska"}
            };

            using (var context = new ApplicationDbContext(ContextOptions))
            {
                IOrchestralBriefcasesService service = new OrchestralBriefcasesService(context,_mapper);
                List<OrchestralBriefcaseDto> actualOrchBriefcasesDto = service.GetAllAsync().Result;

                Assert.True(actualOrchBriefcasesDto.Count == 3);
                Assert.True(CustomComparer<OrchestralBriefcaseDto>.CheckIfPropertiesAreEqual(
                    expectedOrchBriefcasesDto[0], actualOrchBriefcasesDto[0]));

                Assert.True(CustomComparer<OrchestralBriefcaseDto>.CheckIfPropertiesAreEqual(
                    expectedOrchBriefcasesDto[1], actualOrchBriefcasesDto[1]));

                Assert.True(CustomComparer<OrchestralBriefcaseDto>.CheckIfPropertiesAreEqual(
                    expectedOrchBriefcasesDto[2], actualOrchBriefcasesDto[2]));
            }
        }
    }
}
