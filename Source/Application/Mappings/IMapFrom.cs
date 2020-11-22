﻿using AutoMapper;

namespace EOrchestralBriefcase.Application.Mappings
{
    public interface IMapFrom<T>
    { 
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType()).ReverseMap();
        }
    }
}
