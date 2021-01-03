using System.Threading.Tasks;
using AutoMapper;
using EOrchestralBriefcase.Application.Exceptions;
using EOrchestralBriefcase.Application.Services;
using EOrchestralBriefcase.Application.UnitTests.Common;
using EOrchestralBriefcase.Infrastructure.Persistance;
using Xunit;

namespace EOrchestralBriefcase.Tests.Application.UnitTests
{
    public class OrchestralBriefcaseServiceTests : ServiceTest, IClassFixture<ServiceTestFixture>
    {
        private readonly IMapper _mapper;
        public OrchestralBriefcaseServiceTests(ServiceTestFixture fixture)
        {
            _mapper = fixture.Mapper;
            DataSeeder.Seed(DbContextOptions);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllOrchestralBriefcaseDtos()
        {
            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var service = new OrchestralBriefcasesService(context, _mapper);

                var actualOrchBriefcasesDto = await service.GetAllAsync();

                Assert.Equal(actualOrchBriefcasesDto.Count, DataSeeder.OrchestralBriefcaseCount);
            }
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOrchestralBriefcaseDto()
        {
            int id = 2;

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var service = new OrchestralBriefcasesService(context, _mapper);

                var orchestralBriefcaseDto = await service.GetByIdAsync(id);

                Assert.True(orchestralBriefcaseDto != null);
                Assert.Equal(orchestralBriefcaseDto.Id, id);
            }
        }

        [Fact]
        public void GetByIdAsync_ShouldRequireValidId()
        {
            int id = 99;

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var service = new OrchestralBriefcasesService(context, _mapper);

                Assert.ThrowsAsync<NotFoundException>(async () => await service.GetByIdAsync(id));
            }
        }
    }
}
