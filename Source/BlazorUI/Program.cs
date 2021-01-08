using System;
using System.Net.Http;
using System.Threading.Tasks;
using EOrchestralBriefcase.BlazorUI.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EOrchestralBriefcase.BlazorUI
{

    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var baseAddress = builder.Configuration.GetValue<string>("BaseUrl");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });

            builder.Services.AddScoped<IOrchestralPiecesService, OrchestralPiecesService>();
            builder.Services.AddScoped<IOrchestralBriefcasesService, OrchestralBriefcasesService>();

            await builder.Build().RunAsync();
        }
    }
}
