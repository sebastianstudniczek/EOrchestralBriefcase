using System.Linq;
using AutoMapper;
using EOrchestralBriefcase.Application.Mappings;
using EOrchestralBriefcase.Domain.Entities;

namespace EOrchestralBriefcase.Application.Dtos
{
    public class OrchestralPieceDto : IMapFrom<OrchestralPiece>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Composer { get; set; }
        public int? Tempo { get; set; }
        public string SongLink { get; set; }
        public int OrchestralBriefcaseId { get; set; }
        public int NumberInBriefcase { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrchestralPiece, OrchestralPieceDto>()
                .ForMember(dest => dest.OrchestralBriefcaseId, opt => opt.MapFrom(
                    src => src.OrchestralBriefcaseLinks.FirstOrDefault().OrchestralBriefcaseId))
                .ForMember(dest => dest.NumberInBriefcase, opt => opt.MapFrom(
                    src => src.OrchestralBriefcaseLinks.FirstOrDefault().NumberInBriefcase));

            profile.CreateMap<OrchestralPieceDto, OrchestralPiece>()
                .ForMember(dest => dest.SheetFiles, opt => opt.Ignore())
                .ForMember(dest => dest.OrchestralBriefcaseLinks, opt => opt.Ignore());
        }
    }
}
