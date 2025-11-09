using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanTemplate.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Updatecustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_Name",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Customers_Name",
                table: "Customers",
                column: "Name",
                unique: true);
        }
    }
}
