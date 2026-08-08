using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarLab.BiddingService.Hosts.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class AddStartingPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "starting_price",
                table: "lots",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "starting_price",
                table: "lots");
        }
    }
}
