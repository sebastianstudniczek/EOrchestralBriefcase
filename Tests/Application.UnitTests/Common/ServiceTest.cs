using EOrchestralBriefcase.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace EOrchestralBriefcase.Application.UnitTests.Common
{
    public class ServiceTest
    {
        public ServiceTest()
        {
            DbContextOptions =
                DbContextOptionsFactory<ApplicationDbContext>.Create();
        }

        public DbContextOptions<ApplicationDbContext> DbContextOptions { get;}
    }
}
