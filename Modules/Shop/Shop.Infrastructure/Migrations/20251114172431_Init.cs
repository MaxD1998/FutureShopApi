using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdCampaign",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdCampaign", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Basket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExternalId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ParentCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsFavourite = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    ExternalId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdCampaignItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AdCampaignId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<string>(type: "text", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    Lang = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdCampaignItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdCampaignItem_AdCampaign_AdCampaignId",
                        column: x => x.AdCampaignId,
                        principalTable: "AdCampaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AdCampaignId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<JsonDocument>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promotion_AdCampaign_AdCampaignId",
                        column: x => x.AdCampaignId,
                        principalTable: "AdCampaign",
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
                    ExternalId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
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
                name: "UserCompanyDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CompanyIdentifierNumber = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    PostalCode = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    HouseNumber = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    ApartamentNumber = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompanyDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCompanyDetails_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDeliveryAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    PostalCode = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    HouseNumber = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    ApartamentNumber = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDeliveryAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDeliveryAddress_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExternalId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProductBaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    WasActive = table.Column<bool>(type: "boolean", nullable: false)
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
                name: "AdCampaignProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AdCampaignId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdCampaignProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdCampaignProduct_AdCampaign_AdCampaignId",
                        column: x => x.AdCampaignId,
                        principalTable: "AdCampaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdCampaignProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasketItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BasketId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItem_Basket_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Basket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Price_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPhoto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExternalId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<string>(type: "text", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPhoto_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReview",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReview", x => x.Id);
                    table.CheckConstraint("CK_ProductReview_Rating_Range", "\"Rating\" BETWEEN 1 AND 5");
                    table.ForeignKey(
                        name: "FK_ProductReview_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReview_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProductTranslation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Lang = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Translation = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTranslation_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PromotionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionProduct_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseListItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PurchaseListId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseListItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseListItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseListItem_PurchaseList_PurchaseListId",
                        column: x => x.PurchaseListId,
                        principalTable: "PurchaseList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductParameterTranslation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Lang = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Translation = table.Column<string>(type: "text", nullable: false),
                    ProductParameterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductParameterTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductParameterTranslation_ProductParameter_ProductParamet~",
                        column: x => x.ProductParameterId,
                        principalTable: "ProductParameter",
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
                    ProductParameterId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
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

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreateTime", "DateOfBirth", "Email", "ExternalId", "FirstName", "LastName", "ModifyTime" },
                values: new object[] { new Guid("d6669a68-5afb-432d-858f-3f5181579a90"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1, 1, 1), null, new Guid("d6669a68-5afb-432d-858f-3f5181579a90"), "Super", "Admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_AdCampaignItem_AdCampaignId_Position_Lang",
                table: "AdCampaignItem",
                columns: new[] { "AdCampaignId", "Position", "Lang" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdCampaignItem_FileId",
                table: "AdCampaignItem",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdCampaignProduct_AdCampaignId",
                table: "AdCampaignProduct",
                column: "AdCampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_AdCampaignProduct_ProductId",
                table: "AdCampaignProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Basket_UserId",
                table: "Basket",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_BasketId_ProductId",
                table: "BasketItem",
                columns: new[] { "BasketId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_ProductId",
                table: "BasketItem",
                column: "ProductId");

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
                name: "IX_Price_ProductId",
                table: "Price",
                column: "ProductId");

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
                name: "IX_ProductParameterTranslation_ProductParameterId_Lang",
                table: "ProductParameterTranslation",
                columns: new[] { "ProductParameterId", "Lang" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductParameterValue_ProductId",
                table: "ProductParameterValue",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductParameterValue_ProductParameterId",
                table: "ProductParameterValue",
                column: "ProductParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPhoto_FileId",
                table: "ProductPhoto",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPhoto_ProductId_Position",
                table: "ProductPhoto",
                columns: new[] { "ProductId", "Position" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_ProductId_UserId",
                table: "ProductReview",
                columns: new[] { "ProductId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_UserId",
                table: "ProductReview",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslation_ProductId_Lang",
                table: "ProductTranslation",
                columns: new[] { "ProductId", "Lang" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_AdCampaignId",
                table: "Promotion",
                column: "AdCampaignId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionProduct_ProductId",
                table: "PromotionProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionProduct_PromotionId",
                table: "PromotionProduct",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseListItem_ProductId",
                table: "PurchaseListItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseListItem_PurchaseListId",
                table: "PurchaseListItem",
                column: "PurchaseListId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanyDetails_UserId",
                table: "UserCompanyDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDeliveryAddress_UserId",
                table: "UserDeliveryAddress",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdCampaignItem");

            migrationBuilder.DropTable(
                name: "AdCampaignProduct");

            migrationBuilder.DropTable(
                name: "BasketItem");

            migrationBuilder.DropTable(
                name: "CategoryTranslation");

            migrationBuilder.DropTable(
                name: "Price");

            migrationBuilder.DropTable(
                name: "ProductParameterTranslation");

            migrationBuilder.DropTable(
                name: "ProductParameterValue");

            migrationBuilder.DropTable(
                name: "ProductPhoto");

            migrationBuilder.DropTable(
                name: "ProductReview");

            migrationBuilder.DropTable(
                name: "ProductTranslation");

            migrationBuilder.DropTable(
                name: "PromotionProduct");

            migrationBuilder.DropTable(
                name: "PurchaseListItem");

            migrationBuilder.DropTable(
                name: "UserCompanyDetails");

            migrationBuilder.DropTable(
                name: "UserDeliveryAddress");

            migrationBuilder.DropTable(
                name: "Basket");

            migrationBuilder.DropTable(
                name: "ProductParameter");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "PurchaseList");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "AdCampaign");

            migrationBuilder.DropTable(
                name: "ProductBase");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
