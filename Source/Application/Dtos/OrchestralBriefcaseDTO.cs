using System.Collections.Generic;
using AutoMapper;
using EOrchestralBriefcase.Application.Mappings;
using EOrchestralBriefcase.Domain.Entities;

namespace EOrchestralBriefcase.Application.Dtos
{
    public class OrchestralBriefcaseDto : IMapFrom<OrchestralBriefcase>
    {
        public OrchestralBriefcaseDto()
        {
            OrchestralPieces = new List<OrchestralPieceDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<OrchestralPieceDto> OrchestralPieces { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrchestralBriefcase, OrchestralBriefcaseDto>()
                .ForMember(dest => dest.OrchestralPieces, opt => opt.Ignore());

            profile.CreateMap<OrchestralBriefcaseDto, OrchestralBriefcase>()
                .ForMember(dest => dest.OrchestralPieceLinks, opt => opt.Ignore());
        }
    }
}
