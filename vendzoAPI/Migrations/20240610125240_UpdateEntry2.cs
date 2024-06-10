using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vendzoAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntry2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "trackingNo",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "orderEntry",
                type: "varchar(13)",
                unicode: false,
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "trackingNo",
                table: "orderEntry",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "orderEntry");

            migrationBuilder.DropColumn(
                name: "trackingNo",
                table: "orderEntry");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Order",
                type: "varchar(13)",
                unicode: false,
                maxLength: 13,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "trackingNo",
                table: "Order",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);
        }
    }
}
