using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CleanTemplate.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    city = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    State = table.Column<int>(type: "integer", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: false),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Name",
                table: "Customers",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
