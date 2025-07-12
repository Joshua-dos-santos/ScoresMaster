using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoresMasterApi_Football.Migrations
{
    /// <inheritdoc />
    public partial class fixCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Players");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
