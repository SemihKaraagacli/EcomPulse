using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcomPulse.Repository.Migrations
{
    /// <inheritdoc />
    public partial class creditcardupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AvailableBalance",
                table: "CreditCards",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableBalance",
                table: "CreditCards");
        }
    }
}
