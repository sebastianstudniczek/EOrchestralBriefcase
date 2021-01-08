using System;
using EOrchestralBriefcase.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EOrchestralBriefcase.Application.UnitTests.Common
{
    public static class DbContextOptionsFactory<TContext> where TContext : DbContext, IApplicationDbContext
    {
        public static DbContextOptions<TContext> Create()
        {
            return new DbContextOptionsBuilder<TContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;
        }
    }
}
