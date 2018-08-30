using Microsoft.EntityFrameworkCore.Migrations;

namespace SenpaiModel.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Size = table.Column<int>(nullable: false),
                    NextRound = table.Column<int>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kanjis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(nullable: true),
                    Sign = table.Column<string>(nullable: true),
                    Meaning = table.Column<string>(nullable: true),
                    Onyomi = table.Column<string>(nullable: true),
                    Kunyomi = table.Column<string>(nullable: true),
                    Example = table.Column<string>(nullable: true),
                    EFactor = table.Column<float>(nullable: false),
                    LastRound = table.Column<int>(nullable: false),
                    NextRound = table.Column<int>(nullable: false),
                    Timestamp = table.Column<int>(nullable: false),
                    LessonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kanjis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kanjis_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(nullable: true),
                    Kana = table.Column<string>(nullable: true),
                    Kanji = table.Column<string>(nullable: true),
                    Translation = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Example = table.Column<string>(nullable: true),
                    EFactorJap = table.Column<float>(nullable: false),
                    LastRoundJap = table.Column<int>(nullable: false),
                    NextRoundJap = table.Column<int>(nullable: false),
                    EFactorGer = table.Column<float>(nullable: false),
                    LastRoundGer = table.Column<int>(nullable: false),
                    NextRoundGer = table.Column<int>(nullable: false),
                    VocabType = table.Column<string>(nullable: true),
                    ShowWord = table.Column<string>(nullable: true),
                    ShowDesc = table.Column<string>(nullable: true),
                    LessonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Words_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kanjis_LessonId",
                table: "Kanjis",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_LessonId",
                table: "Words",
                column: "LessonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kanjis");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Lessons");
        }
    }
}
