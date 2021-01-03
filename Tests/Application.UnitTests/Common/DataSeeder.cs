using System.Collections.Generic;
using EOrchestralBriefcase.Domain.Entities;
using EOrchestralBriefcase.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace EOrchestralBriefcase.Application.UnitTests.Common
{
    internal static class DataSeeder
    {
        internal static int OrchestralBriefcaseCount { get; private set; }
        internal static int OrchestralPieceCount { get; private set; }

        internal static void Seed(DbContextOptions<ApplicationDbContext> options)
        {
            using (var context = new ApplicationDbContext(options))
            {
                var orchestralBriefcases = new List<OrchestralBriefcase>
                {
                    new OrchestralBriefcase { Id = 1, Name = "Czerwona" },
                    new OrchestralBriefcase { Id = 2, Name = "Czarna" },
                    new OrchestralBriefcase { Id = 3, Name = "Niebieska"}
                };

                OrchestralBriefcaseCount = orchestralBriefcases.Count;

                var orchestralPieces = new List<OrchestralPiece>
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

                OrchestralPieceCount = orchestralPieces.Count;
                int numberInBriefcase = 1;

                var orchestralBriefcaseOrchestralPiece = new List<OrchestralBriefcaseOrchestralPiece>();

                for (int i = 0; i < orchestralPieces.Count; i++)
                {
                    orchestralBriefcaseOrchestralPiece.Add(
                        new OrchestralBriefcaseOrchestralPiece
                        {
                            NumberInOrchestralBriefcase = numberInBriefcase,
                            OrchestralBriefcase = orchestralBriefcases[0],
                            OrchestralPiece = orchestralPieces[i]
                        }
                    );
                    numberInBriefcase++;
                }

                context.OrchestralPieces.AddRange(orchestralPieces);
                context.OrchestralBriefcases.AddRange(orchestralBriefcases);
                context.OrchestralBriefcaseOrchestralPiece.AddRange(orchestralBriefcaseOrchestralPiece);
                context.SaveChanges();
            }
        }
    }
}
