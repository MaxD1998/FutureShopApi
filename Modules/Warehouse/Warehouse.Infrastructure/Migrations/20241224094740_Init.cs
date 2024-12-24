using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    BuildingName = table.Column<string>(type: "text", nullable: false),
                    FlatNumber = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoodsIssue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsIssue_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceipt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsReceipt_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterWarehouseTransfer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SourceWarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    DestinationWarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateOfReceipt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterWarehouseTransfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterWarehouseTransfer_Warehouse_DestinationWarehouseId",
                        column: x => x.DestinationWarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterWarehouseTransfer_Warehouse_SourceWarehouseId",
                        column: x => x.SourceWarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductQuantity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQuantity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductQuantity_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductQuantity_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsIssueProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    GoodsIssueId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsIssueProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsIssueProducts_GoodsIssue_GoodsIssueId",
                        column: x => x.GoodsIssueId,
                        principalTable: "GoodsIssue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsIssueProducts_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceiptProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    GoodsReceiptId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceiptProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsReceiptProduct_GoodsReceipt_GoodsReceiptId",
                        column: x => x.GoodsReceiptId,
                        principalTable: "GoodsReceipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsReceiptProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterWarehouseTransferProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InterWarehouseTrasferId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterWarehouseTransferProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterWarehouseTransferProducts_InterWarehouseTransfer_Inter~",
                        column: x => x.InterWarehouseTrasferId,
                        principalTable: "InterWarehouseTransfer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterWarehouseTransferProducts_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssue_WarehouseId",
                table: "GoodsIssue",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueProducts_GoodsIssueId",
                table: "GoodsIssueProducts",
                column: "GoodsIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueProducts_ProductId",
                table: "GoodsIssueProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceipt_WarehouseId",
                table: "GoodsReceipt",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptProduct_GoodsReceiptId",
                table: "GoodsReceiptProduct",
                column: "GoodsReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptProduct_ProductId",
                table: "GoodsReceiptProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InterWarehouseTransfer_DestinationWarehouseId",
                table: "InterWarehouseTransfer",
                column: "DestinationWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InterWarehouseTransfer_SourceWarehouseId",
                table: "InterWarehouseTransfer",
                column: "SourceWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InterWarehouseTransferProducts_InterWarehouseTrasferId",
                table: "InterWarehouseTransferProducts",
                column: "InterWarehouseTrasferId");

            migrationBuilder.CreateIndex(
                name: "IX_InterWarehouseTransferProducts_ProductId",
                table: "InterWarehouseTransferProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantity_ProductId",
                table: "ProductQuantity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantity_WarehouseId",
                table: "ProductQuantity",
                column: "WarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsIssueProducts");

            migrationBuilder.DropTable(
                name: "GoodsReceiptProduct");

            migrationBuilder.DropTable(
                name: "InterWarehouseTransferProducts");

            migrationBuilder.DropTable(
                name: "ProductQuantity");

            migrationBuilder.DropTable(
                name: "GoodsIssue");

            migrationBuilder.DropTable(
                name: "GoodsReceipt");

            migrationBuilder.DropTable(
                name: "InterWarehouseTransfer");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Warehouse");
        }
    }
}
