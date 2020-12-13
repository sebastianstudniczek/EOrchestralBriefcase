using System.Collections.Generic;
using EOrchestralBriefcase.Domain.Entities;
using EOrchestralBriefcase.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace EOrchestralBriefcase.Tests.Application.UnitTests.Helper
{
    internal static class DataSeeder
    {
        internal static void Seed(DbContextOptions<ApplicationDbContext> options)
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();

                List<OrchestralBriefcase> orchBriefcases = new List<OrchestralBriefcase>
                {
                    new OrchestralBriefcase { Id = 1, Name = "Czerwona" },
                    new OrchestralBriefcase { Id = 2, Name = "Czarna" },
                    new OrchestralBriefcase { Id = 3, Name = "Niebieska"}
                };

                List<OrchestralPiece> orchPieces = new List<OrchestralPiece>
                {
                    new OrchestralPiece
                    {
                        Id = 1,
                        Title = "ABBA GOLD",
                        Composer = "Ron Sebregets",
                        SongLink = "https://www.youtube.com/watch?v=3P9NC4dB89Y"
                    },
                    new OrchestralPiece
                    {
                        Id = 2,
                        Title = "Adria",
                        Composer = "Josef Raha"
                    },
                    new OrchestralPiece
                    {
                        Id = 3,
                        Title = "Alte Kameraden Swing"
                    },
                    new OrchestralPiece
                    {
                        Id = 4,
                        Title = "Bohemian Rhapsody",
                        Composer = "Freddie Mercury",
                        SongLink = "https://www.youtube.com/watch?v=rhmDm42Eljs"
                    },
                };

                int numberInBriefcase = 1;

                var orchestralBriefcaseOrchestralPiece = new List<OrchestralBriefcaseOrchestralPiece>();

                for (int i = 0; i < orchPieces.Count; i++)
                {
                    orchestralBriefcaseOrchestralPiece.Add(
                        new OrchestralBriefcaseOrchestralPiece
                        {
                            NumberInBriefcase = numberInBriefcase,
                            OrchestralBriefcase = orchBriefcases[0],
                            OrchestralPiece = orchPieces[i]
                        }
                    );
                    numberInBriefcase++;
                }

                context.OrchestralPieces.AddRange(orchPieces);
                context.OrchestralBriefcases.AddRange(orchBriefcases);
                context.OrchestralBriefcaseOrchestralPiece.AddRange(orchestralBriefcaseOrchestralPiece);
                context.SaveChanges();
            }
        }
    }
}
