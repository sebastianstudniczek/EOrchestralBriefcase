using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EOrchestralBriefcase.Domain.Entities;

namespace EOrchestralBriefcase.Infrastructure.Persistance
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.OrchestralBriefcases.Any())
            {
                List<OrchestralBriefcase> orchestralBriefcases = new List<OrchestralBriefcase>
                {
                    new OrchestralBriefcase { Name = "Czerwona" },
                    new OrchestralBriefcase { Name = "Czarna" },
                };

                var orchestralPiecesRed = new List<OrchestralPiece>
                {
                    new OrchestralPiece { Title = "ABBA GOLD", Composer = "Ron Sebregets", SongLink = "https://www.youtube.com/watch?v=3P9NC4dB89Y" },
                    new OrchestralPiece { Title = "Adria", Composer = "Josef Raha" },
                    new OrchestralPiece { Title = "Alte Kameraden Swing"},
                    new OrchestralPiece { Title = "Bohemian Rhapsody", Composer = "Freddie Mercury", SongLink = "https://www.youtube.com/watch?v=rhmDm42Eljs" },
                };

                var orchestralPiecesBlack = new List<OrchestralPiece>
                {
                    new OrchestralPiece { Title = "Highlights from Carmen"},
                    new OrchestralPiece { Title = "Chorus of Hebrew Slaves"},
                    new OrchestralPiece { Title = "Symphonic Chorales"},
                    new OrchestralPiece { Title = "Habanera"},
                };

                var orchestralPieceLinks = new List<OrchestralBriefcaseOrchestralPiece>
                {
                    new OrchestralBriefcaseOrchestralPiece
                    {
                        NumberInOrchestralBriefcase = 1,
                        OrchestralBriefcase = orchestralBriefcases[0],
                        OrchestralPiece = orchestralPiecesRed[0],
                    },
                    new OrchestralBriefcaseOrchestralPiece
                    {
                        NumberInOrchestralBriefcase = 2,
                        OrchestralBriefcase = orchestralBriefcases[0],
                        OrchestralPiece = orchestralPiecesRed[1],
                    },
                    new OrchestralBriefcaseOrchestralPiece
                    {
                        NumberInOrchestralBriefcase = 3,
                        OrchestralBriefcase = orchestralBriefcases[0],
                        OrchestralPiece = orchestralPiecesRed[2],
                    },
                    new OrchestralBriefcaseOrchestralPiece
                    {
                        NumberInOrchestralBriefcase = 4,
                        OrchestralBriefcase = orchestralBriefcases[0],
                        OrchestralPiece = orchestralPiecesRed[3],
                    },
                    new OrchestralBriefcaseOrchestralPiece
                    {
                        NumberInOrchestralBriefcase = 1,
                        OrchestralBriefcase = orchestralBriefcases[1],
                        OrchestralPiece = orchestralPiecesBlack[0],
                    },
                    new OrchestralBriefcaseOrchestralPiece
                    {
                        NumberInOrchestralBriefcase = 1,
                        OrchestralBriefcase = orchestralBriefcases[1],
                        OrchestralPiece = orchestralPiecesBlack[1],
                    },
                    new OrchestralBriefcaseOrchestralPiece
                    {
                        NumberInOrchestralBriefcase = 1,
                        OrchestralBriefcase = orchestralBriefcases[1],
                        OrchestralPiece = orchestralPiecesBlack[2],
                    },
                    new OrchestralBriefcaseOrchestralPiece
                    {
                        NumberInOrchestralBriefcase = 1,
                        OrchestralBriefcase = orchestralBriefcases[1],
                        OrchestralPiece = orchestralPiecesBlack[3],
                    },
                };

                context.OrchestralPieces.AddRange(orchestralPiecesRed);
                context.OrchestralPieces.AddRange(orchestralPiecesBlack);
                context.OrchestralBriefcases.AddRange(orchestralBriefcases);
                context.OrchestralBriefcaseOrchestralPiece.AddRange(orchestralPieceLinks);

                await context
                    .SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }
    }
}
