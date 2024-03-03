using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ParentCategoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CategoryTranslation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Lang = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Translation = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryTranslation_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductBase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductBase_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProductBaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductBase_ProductBaseId",
                        column: x => x.ProductBaseId,
                        principalTable: "ProductBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductParameter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProductBaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductParameter_ProductBase_ProductBaseId",
                        column: x => x.ProductBaseId,
                        principalTable: "ProductBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductParameterValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ProductParameterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductParameterValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductParameterValue_ProductParameter_ProductParameterId",
                        column: x => x.ProductParameterId,
                        principalTable: "ProductParameter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductParameterValue_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslation_CategoryId_Lang",
                table: "CategoryTranslation",
                columns: new[] { "CategoryId", "Lang" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductBaseId",
                table: "Product",
                column: "ProductBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBase_CategoryId",
                table: "ProductBase",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductParameter_ProductBaseId",
                table: "ProductParameter",
                column: "ProductBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductParameterValue_ProductId",
                table: "ProductParameterValue",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductParameterValue_ProductParameterId",
                table: "ProductParameterValue",
                column: "ProductParameterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryTranslation");

            migrationBuilder.DropTable(
                name: "ProductParameterValue");

            migrationBuilder.DropTable(
                name: "ProductParameter");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductBase");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
