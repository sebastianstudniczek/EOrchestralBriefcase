using System.Collections.Generic;
using System.ComponentModel;
using AutoMapper;
using EOrchestralBriefcase.Application.Dtos.OrchestralBriefcases;
using EOrchestralBriefcase.Application.Mappings;

namespace EOrchestralBriefcase.Application.ViewModels
{
    public class OrchestralBriefcaseVm : IMapFrom<OrchestralBriefcaseCreateDto>
    {
        public int Id { get; set; }

        [DisplayName("Nazwa")]
        public string Name { get; set; }

        public IList<OrchestralPieceVm> OrchestralPieces { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrchestralBriefcaseVm, OrchestralBriefcaseCreateDto>()
                .ReverseMap();
        }
    }
}
