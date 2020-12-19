using System.ComponentModel;
using AutoMapper;
using EOrchestralBriefcase.Application.Dtos;
using EOrchestralBriefcase.Application.Mappings;

namespace EOrchestralBriefcase.Application.ViewModels
{
    public class OrchestralPieceVm  : IMapFrom<OrchestralPieceReadDto>
    {
        public int Id { get; set; }


        [DisplayName("Tytuł")]
        public string Title { get; set; }


        [DisplayName("Kompozytor")]
        public string Composer { get; set; }

        public int? Tempo { get; set; }


        [DisplayName("Link do utworu")]
        public string SongLink { get; set; }


        [DisplayName("Numer w teczce")]
        public int NumberInBriefcase { get; set; }

        public int OrchestralBriefcaseId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrchestralPieceVm, OrchestralPieceReadDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
