using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vendzoAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtAndIsDeletedToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    promoCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    amount = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    expires = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    type = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Promotio__3213E83F535209CC", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    userId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true),
                    contactNo = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: true),
                    address = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Address__3213E83F29F4F205", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    pass = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    currentAddress = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true),
                    contactNo = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: true),
                    userType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__3213E83FD4F6D364", x => x.id);
                    table.ForeignKey(
                        name: "FK_currentAddress",
                        column: x => x.currentAddress,
                        principalTable: "Address",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    sellerId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true),
                    description = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    title = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: true),
                    category = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: true),
                    price = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    photo = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    stock = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Item__3213E83F2E15E9F0", x => x.id);
                    table.ForeignKey(
                        name: "FK__Item__sellerId__4F7CD00D",
                        column: x => x.sellerId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    userId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true),
                    orderDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    shipDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    deliverDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    shipAddress = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    trackingNo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    status = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: true),
                    total = table.Column<decimal>(type: "numeric(9,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__3213E83FCC5800CF", x => x.id);
                    table.ForeignKey(
                        name: "FK__Order__userId__5AEE82B9",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Basket",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    userId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true),
                    itemId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Basket__3213E83FDDDA08BF", x => x.id);
                    table.ForeignKey(
                        name: "FK__Basket__itemId__571DF1D5",
                        column: x => x.itemId,
                        principalTable: "Item",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Basket__userId__5629CD9C",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_userId",
                table: "Address",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Basket_itemId",
                table: "Basket",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "IX_Basket_userId",
                table: "Basket",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_sellerId",
                table: "Item",
                column: "sellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_userId",
                table: "Order",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_User_currentAddress",
                table: "User",
                column: "currentAddress");

            migrationBuilder.AddForeignKey(
                name: "FK__Address__userId__4CA06362",
                table: "Address",
                column: "userId",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Address__userId__4CA06362",
                table: "Address");

            migrationBuilder.DropTable(
                name: "Basket");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
