using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistance.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrchestralBriefcases",
                columns: table => new
                {
                    OrchestralBriefcaseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrchestralBriefcases", x => x.OrchestralBriefcaseID);
                });

            migrationBuilder.CreateTable(
                name: "OrchestralPieces",
                columns: table => new
                {
                    OrchestralPieceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 80, nullable: false),
                    Composer = table.Column<string>(maxLength: 80, nullable: true),
                    Tempo = table.Column<int>(nullable: true),
                    SongLink = table.Column<string>(unicode: false, maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrchestralPieces", x => x.OrchestralPieceID);
                });

            migrationBuilder.CreateTable(
                name: "OrchestralBriefcaseOrchestralPiece",
                columns: table => new
                {
                    OrchestralBriefcaseId = table.Column<int>(nullable: false),
                    OrchestralPieceId = table.Column<int>(nullable: false),
                    NumberInBriefcase = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrchestralBriefcaseOrchestralPiece", x => new { x.OrchestralBriefcaseId, x.OrchestralPieceId });
                    table.ForeignKey(
                        name: "FK_OrchestralBriefcaseOrchestralPiece_OrchestralBriefcases_OrchestralBriefcaseId",
                        column: x => x.OrchestralBriefcaseId,
                        principalTable: "OrchestralBriefcases",
                        principalColumn: "OrchestralBriefcaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrchestralBriefcaseOrchestralPiece_OrchestralPieces_OrchestralPieceId",
                        column: x => x.OrchestralPieceId,
                        principalTable: "OrchestralPieces",
                        principalColumn: "OrchestralPieceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SheetFiles",
                columns: table => new
                {
                    SheetFileID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 80, nullable: false),
                    FileData = table.Column<byte[]>(nullable: true),
                    OrchestralPieceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheetFiles", x => x.SheetFileID);
                    table.ForeignKey(
                        name: "FK_SheetFiles_OrchestralPieces_OrchestralPieceId",
                        column: x => x.OrchestralPieceId,
                        principalTable: "OrchestralPieces",
                        principalColumn: "OrchestralPieceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrchestralBriefcaseOrchestralPiece_OrchestralPieceId",
                table: "OrchestralBriefcaseOrchestralPiece",
                column: "OrchestralPieceId");

            migrationBuilder.CreateIndex(
                name: "IX_SheetFiles_OrchestralPieceId",
                table: "SheetFiles",
                column: "OrchestralPieceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrchestralBriefcaseOrchestralPiece");

            migrationBuilder.DropTable(
                name: "SheetFiles");

            migrationBuilder.DropTable(
                name: "OrchestralBriefcases");

            migrationBuilder.DropTable(
                name: "OrchestralPieces");
        }
    }
}
