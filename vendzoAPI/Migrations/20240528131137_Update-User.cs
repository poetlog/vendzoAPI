using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vendzoAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "pass",
                table: "User",
                newName: "loginId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "loginId",
                table: "User",
                newName: "pass");
        }
    }
}
