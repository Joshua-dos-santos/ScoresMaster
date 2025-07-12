using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoresMasterApi_Football.Migrations
{
    /// <inheritdoc />
    public partial class MakeCountryCodeUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    FlagUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_CountryId",
                table: "Players",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Code",
                table: "Countries",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Countries_CountryId",
                table: "Players",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Countries_CountryId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Players_CountryId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Players");
        }
    }
}
