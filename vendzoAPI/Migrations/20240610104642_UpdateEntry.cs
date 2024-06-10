using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vendzoAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "itemTitle",
                table: "orderEntry",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sellerName",
                table: "orderEntry",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "itemTitle",
                table: "orderEntry");

            migrationBuilder.DropColumn(
                name: "sellerName",
                table: "orderEntry");
        }
    }
}
