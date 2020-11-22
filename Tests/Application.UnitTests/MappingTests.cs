using AutoMapper;
using EOrchestralBriefcase.Application.Dtos;
using EOrchestralBriefcase.Application.Mappings;
using EOrchestralBriefcase.Domain.Entities;
using System;
using Xunit;

namespace EOrchestralBriefcase.Tests.Application.UnitTests
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(OrchestralBriefcase), typeof(OrchestralBriefcaseDto))]
        [InlineData(typeof(OrchestralPiece), typeof(OrchestralPieceDto))]
        public void ShouldMapFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }

        [Theory]
        [InlineData(typeof(OrchestralBriefcaseDto), typeof(OrchestralBriefcase))]
        [InlineData(typeof(OrchestralPieceDto), typeof(OrchestralPiece))]
        public void ShouldMapFromDestinationToSource(Type destination, Type source)
        {
            var instance = Activator.CreateInstance(destination);

            _mapper.Map(instance, destination, source);
        }
    }
}
