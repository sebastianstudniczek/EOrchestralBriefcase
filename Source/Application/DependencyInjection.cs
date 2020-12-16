using AutoMapper;
using EOrchestralBriefcase.Application.Interfaces;
using EOrchestralBriefcase.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EOrchestralBriefcase.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<IOrchestralBriefcasesService, OrchestralBriefcasesService>();
            services.AddTransient<IOrchestralPiecesService, OrchestralPiecesService>();

            return services;
        }
    }
}
