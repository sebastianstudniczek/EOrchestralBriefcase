using EOrchestralBriefcase.Application;
using EOrchestralBriefcase.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EOrchestralBriefcase.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddCors(options =>
            {
                options.AddPolicy("Open", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            services.AddControllers().AddNewtonsoftJson();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSwaggerDocument(configure =>
            {
                configure.Title = "EOrchestral Briefcase API";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("Open");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
