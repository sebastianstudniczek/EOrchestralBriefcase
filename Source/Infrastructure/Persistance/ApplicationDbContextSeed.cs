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
                    new OrchestralPiece { Title = "Amazing Grace"},
                    new OrchestralPiece { Title = "Taniec Podhalańczyków"},
                };

                var orchestralPieceLinks = new List<OrchestralBriefcaseOrchestralPiece>();
                var redBriefcaseLinks = AddOrchestralPieceToOrchestralBriefcase(orchestralBriefcases[0], orchestralPiecesRed);
                var blackBriefcaseLinks = AddOrchestralPieceToOrchestralBriefcase(orchestralBriefcases[1], orchestralPiecesBlack);

                orchestralPieceLinks.AddRange(redBriefcaseLinks);
                orchestralPieceLinks.AddRange(blackBriefcaseLinks);

                context.OrchestralPieces.AddRange(orchestralPiecesRed);
                context.OrchestralPieces.AddRange(orchestralPiecesBlack);
                context.OrchestralBriefcases.AddRange(orchestralBriefcases);
                context.OrchestralBriefcaseOrchestralPiece.AddRange(orchestralPieceLinks);

                await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        private static IEnumerable<OrchestralBriefcaseOrchestralPiece> AddOrchestralPieceToOrchestralBriefcase(OrchestralBriefcase orchestralBriefcase ,List<OrchestralPiece> orchestralPieces)
        {
            for (int i = 0; i < orchestralPieces.Count; i++)
            {
                yield return
                    new OrchestralBriefcaseOrchestralPiece
                    {
                        NumberInBriefcase = i + 1,
                        OrchestralBriefcase = orchestralBriefcase,
                        OrchestralPiece = orchestralPieces[i]
                    };
            }
        }

    }
}
