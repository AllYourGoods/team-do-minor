using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllYourGoods.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserRolesRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Menus_MenuId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProducts_Categories_CategoryId",
                table: "CategoryProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProducts_Products_ProductId",
                table: "CategoryProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryPersons_Users_UserId",
                table: "DeliveryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_FAQs_Restaurants_RestaurantId",
                table: "FAQs");

            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Restaurants_RestaurantId",
                table: "Menus");

            migrationBuilder.DropForeignKey(
                name: "FK_OpeningsTimes_Restaurants_RestaurantId",
                table: "OpeningsTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_AddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Restaurants_RestaurantId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_DeliveryPersonId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ImageFiles_ImageFileId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Products_ProductId",
                table: "ProductTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Tags_TagId",
                table: "ProductTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Addresses_AddressId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_ImageFiles_BannerId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_ImageFiles_LogoId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Users_OwnerId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantTags_Restaurants_RestaurantId",
                table: "RestaurantTags");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantTags_Tags_TagId",
                table: "RestaurantTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantTags",
                table: "RestaurantTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTags",
                table: "ProductTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpeningsTimes",
                table: "OpeningsTimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menus",
                table: "Menus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageFiles",
                table: "ImageFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FAQs",
                table: "FAQs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryPersons",
                table: "DeliveryPersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryProducts",
                table: "CategoryProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "RestaurantTags",
                newName: "RestaurantHasTags");

            migrationBuilder.RenameTable(
                name: "Restaurants",
                newName: "Restaurant");

            migrationBuilder.RenameTable(
                name: "ProductTags",
                newName: "ProductHasTag");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "OrderProducts",
                newName: "OrderHasProduct");

            migrationBuilder.RenameTable(
                name: "OpeningsTimes",
                newName: "OpeningsTime");

            migrationBuilder.RenameTable(
                name: "Menus",
                newName: "Menu");

            migrationBuilder.RenameTable(
                name: "ImageFiles",
                newName: "ImageFile");

            migrationBuilder.RenameTable(
                name: "FAQs",
                newName: "FrequentlyAskedQuestion");

            migrationBuilder.RenameTable(
                name: "DeliveryPersons",
                newName: "DeliveryPerson");

            migrationBuilder.RenameTable(
                name: "CategoryProducts",
                newName: "CategoryHasProduct");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantTags_TagId",
                table: "RestaurantHasTags",
                newName: "IX_RestaurantHasTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurants_OwnerId",
                table: "Restaurant",
                newName: "IX_Restaurant_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurants_LogoId",
                table: "Restaurant",
                newName: "IX_Restaurant_LogoId");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurants_BannerId",
                table: "Restaurant",
                newName: "IX_Restaurant_BannerId");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurants_AddressId",
                table: "Restaurant",
                newName: "IX_Restaurant_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTags_TagId",
                table: "ProductHasTag",
                newName: "IX_ProductHasTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ImageFileId",
                table: "Product",
                newName: "IX_Product_ImageFileId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_RestaurantId",
                table: "Order",
                newName: "IX_Order_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DeliveryPersonId",
                table: "Order",
                newName: "IX_Order_DeliveryPersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Order",
                newName: "IX_Order_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_AddressId",
                table: "Order",
                newName: "IX_Order_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderHasProduct",
                newName: "IX_OrderHasProduct_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OpeningsTimes_RestaurantId",
                table: "OpeningsTime",
                newName: "IX_OpeningsTime_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Menus_RestaurantId",
                table: "Menu",
                newName: "IX_Menu_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_FAQs_RestaurantId",
                table: "FrequentlyAskedQuestion",
                newName: "IX_FrequentlyAskedQuestion_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryPersons_UserId",
                table: "DeliveryPerson",
                newName: "IX_DeliveryPerson_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryProducts_ProductId",
                table: "CategoryHasProduct",
                newName: "IX_CategoryHasProduct_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_MenuId",
                table: "Category",
                newName: "IX_Category_MenuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantHasTags",
                table: "RestaurantHasTags",
                columns: new[] { "RestaurantId", "TagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurant",
                table: "Restaurant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductHasTag",
                table: "ProductHasTag",
                columns: new[] { "ProductId", "TagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderHasProduct",
                table: "OrderHasProduct",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpeningsTime",
                table: "OpeningsTime",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menu",
                table: "Menu",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageFile",
                table: "ImageFile",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FrequentlyAskedQuestion",
                table: "FrequentlyAskedQuestion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryPerson",
                table: "DeliveryPerson",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryHasProduct",
                table: "CategoryHasProduct",
                columns: new[] { "CategoryId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Menu_MenuId",
                table: "Category",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryHasProduct_Category_CategoryId",
                table: "CategoryHasProduct",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryHasProduct_Product_ProductId",
                table: "CategoryHasProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryPerson_User_UserId",
                table: "DeliveryPerson",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FrequentlyAskedQuestion_Restaurant_RestaurantId",
                table: "FrequentlyAskedQuestion",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Restaurant_RestaurantId",
                table: "Menu",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningsTime_Restaurant_RestaurantId",
                table: "OpeningsTime",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Address_AddressId",
                table: "Order",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Restaurant_RestaurantId",
                table: "Order",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_DeliveryPersonId",
                table: "Order",
                column: "DeliveryPersonId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHasProduct_Order_OrderId",
                table: "OrderHasProduct",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHasProduct_Product_ProductId",
                table: "OrderHasProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ImageFile_ImageFileId",
                table: "Product",
                column: "ImageFileId",
                principalTable: "ImageFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHasTag_Product_ProductId",
                table: "ProductHasTag",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHasTag_Tag_TagId",
                table: "ProductHasTag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_Address_AddressId",
                table: "Restaurant",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_ImageFile_BannerId",
                table: "Restaurant",
                column: "BannerId",
                principalTable: "ImageFile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_ImageFile_LogoId",
                table: "Restaurant",
                column: "LogoId",
                principalTable: "ImageFile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_User_OwnerId",
                table: "Restaurant",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantHasTags_Restaurant_RestaurantId",
                table: "RestaurantHasTags",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantHasTags_Tag_TagId",
                table: "RestaurantHasTags",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Menu_MenuId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryHasProduct_Category_CategoryId",
                table: "CategoryHasProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryHasProduct_Product_ProductId",
                table: "CategoryHasProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryPerson_User_UserId",
                table: "DeliveryPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_FrequentlyAskedQuestion_Restaurant_RestaurantId",
                table: "FrequentlyAskedQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Restaurant_RestaurantId",
                table: "Menu");

            migrationBuilder.DropForeignKey(
                name: "FK_OpeningsTime_Restaurant_RestaurantId",
                table: "OpeningsTime");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Address_AddressId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Restaurant_RestaurantId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_DeliveryPersonId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHasProduct_Order_OrderId",
                table: "OrderHasProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHasProduct_Product_ProductId",
                table: "OrderHasProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ImageFile_ImageFileId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductHasTag_Product_ProductId",
                table: "ProductHasTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductHasTag_Tag_TagId",
                table: "ProductHasTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_Address_AddressId",
                table: "Restaurant");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_ImageFile_BannerId",
                table: "Restaurant");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_ImageFile_LogoId",
                table: "Restaurant");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_User_OwnerId",
                table: "Restaurant");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantHasTags_Restaurant_RestaurantId",
                table: "RestaurantHasTags");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantHasTags_Tag_TagId",
                table: "RestaurantHasTags");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantHasTags",
                table: "RestaurantHasTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurant",
                table: "Restaurant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductHasTag",
                table: "ProductHasTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderHasProduct",
                table: "OrderHasProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpeningsTime",
                table: "OpeningsTime");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menu",
                table: "Menu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageFile",
                table: "ImageFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FrequentlyAskedQuestion",
                table: "FrequentlyAskedQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryPerson",
                table: "DeliveryPerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryHasProduct",
                table: "CategoryHasProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "RestaurantHasTags",
                newName: "RestaurantTags");

            migrationBuilder.RenameTable(
                name: "Restaurant",
                newName: "Restaurants");

            migrationBuilder.RenameTable(
                name: "ProductHasTag",
                newName: "ProductTags");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "OrderHasProduct",
                newName: "OrderProducts");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "OpeningsTime",
                newName: "OpeningsTimes");

            migrationBuilder.RenameTable(
                name: "Menu",
                newName: "Menus");

            migrationBuilder.RenameTable(
                name: "ImageFile",
                newName: "ImageFiles");

            migrationBuilder.RenameTable(
                name: "FrequentlyAskedQuestion",
                newName: "FAQs");

            migrationBuilder.RenameTable(
                name: "DeliveryPerson",
                newName: "DeliveryPersons");

            migrationBuilder.RenameTable(
                name: "CategoryHasProduct",
                newName: "CategoryProducts");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantHasTags_TagId",
                table: "RestaurantTags",
                newName: "IX_RestaurantTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurant_OwnerId",
                table: "Restaurants",
                newName: "IX_Restaurants_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurant_LogoId",
                table: "Restaurants",
                newName: "IX_Restaurants_LogoId");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurant_BannerId",
                table: "Restaurants",
                newName: "IX_Restaurants_BannerId");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurant_AddressId",
                table: "Restaurants",
                newName: "IX_Restaurants_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductHasTag_TagId",
                table: "ProductTags",
                newName: "IX_ProductTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ImageFileId",
                table: "Products",
                newName: "IX_Products_ImageFileId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHasProduct_ProductId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_RestaurantId",
                table: "Orders",
                newName: "IX_Orders_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_DeliveryPersonId",
                table: "Orders",
                newName: "IX_Orders_DeliveryPersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_AddressId",
                table: "Orders",
                newName: "IX_Orders_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_OpeningsTime_RestaurantId",
                table: "OpeningsTimes",
                newName: "IX_OpeningsTimes_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Menu_RestaurantId",
                table: "Menus",
                newName: "IX_Menus_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_FrequentlyAskedQuestion_RestaurantId",
                table: "FAQs",
                newName: "IX_FAQs_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryPerson_UserId",
                table: "DeliveryPersons",
                newName: "IX_DeliveryPersons_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryHasProduct_ProductId",
                table: "CategoryProducts",
                newName: "IX_CategoryProducts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_MenuId",
                table: "Categories",
                newName: "IX_Categories_MenuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantTags",
                table: "RestaurantTags",
                columns: new[] { "RestaurantId", "TagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTags",
                table: "ProductTags",
                columns: new[] { "ProductId", "TagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpeningsTimes",
                table: "OpeningsTimes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menus",
                table: "Menus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageFiles",
                table: "ImageFiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FAQs",
                table: "FAQs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryPersons",
                table: "DeliveryPersons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryProducts",
                table: "CategoryProducts",
                columns: new[] { "CategoryId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Menus_MenuId",
                table: "Categories",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProducts_Categories_CategoryId",
                table: "CategoryProducts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProducts_Products_ProductId",
                table: "CategoryProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryPersons_Users_UserId",
                table: "DeliveryPersons",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FAQs_Restaurants_RestaurantId",
                table: "FAQs",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Restaurants_RestaurantId",
                table: "Menus",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningsTimes_Restaurants_RestaurantId",
                table: "OpeningsTimes",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_AddressId",
                table: "Orders",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Restaurants_RestaurantId",
                table: "Orders",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_DeliveryPersonId",
                table: "Orders",
                column: "DeliveryPersonId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ImageFiles_ImageFileId",
                table: "Products",
                column: "ImageFileId",
                principalTable: "ImageFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Products_ProductId",
                table: "ProductTags",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Tags_TagId",
                table: "ProductTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Addresses_AddressId",
                table: "Restaurants",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_ImageFiles_BannerId",
                table: "Restaurants",
                column: "BannerId",
                principalTable: "ImageFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_ImageFiles_LogoId",
                table: "Restaurants",
                column: "LogoId",
                principalTable: "ImageFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Users_OwnerId",
                table: "Restaurants",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantTags_Restaurants_RestaurantId",
                table: "RestaurantTags",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantTags_Tags_TagId",
                table: "RestaurantTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
