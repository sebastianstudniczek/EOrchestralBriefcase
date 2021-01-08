using AutoMapper;
using EOrchestralBriefcase.Application.Mappings;

namespace EOrchestralBriefcase.Application.UnitTests.Common
{
    public sealed class ServiceTestFixture
    {
        public ServiceTestFixture()
        {
            var configurationProvider = new MapperConfiguration(config =>
                config.AddProfile<MappingProfile>());
            Mapper = configurationProvider.CreateMapper();
        }

        public IMapper Mapper { get; }
    }
}
