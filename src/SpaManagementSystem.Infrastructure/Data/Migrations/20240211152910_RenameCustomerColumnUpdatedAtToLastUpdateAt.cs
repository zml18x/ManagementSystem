using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaManagementSystem.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameCustomerColumnUpdatedAtToLastUpdateAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                schema: "SMS",
                table: "Customers",
                newName: "LastUpdateAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdateAt",
                schema: "SMS",
                table: "Customers",
                newName: "UpdatedAt");
        }
    }
}
