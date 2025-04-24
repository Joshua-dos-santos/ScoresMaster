using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoresMasterApi_Football.Migrations
{
    /// <inheritdoc />
    public partial class addleaguetoteam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "Teams",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Teams");
        }
    }
}
