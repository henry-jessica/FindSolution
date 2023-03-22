﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductModel.Migrations
{
    public partial class AddingGRNTableData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GRNLines",
                columns: table => new
                {
                    LineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrnId = table.Column<int>(type: "int", nullable: false),
                    StockID = table.Column<int>(type: "int", nullable: false),
                    QtyDelivered = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRNLines", x => x.LineID);
                });

            migrationBuilder.CreateTable(
                name: "GRNs",
                columns: table => new
                {
                    GrnID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockUpdated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRNs", x => x.GrnID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReorderLevel = table.Column<int>(type: "int", nullable: false),
                    ReorderQuantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false),
                    StockOnHand = table.Column<int>(type: "int", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID");
                });

            migrationBuilder.InsertData(
                table: "GRNLines",
                columns: new[] { "LineID", "GrnId", "QtyDelivered", "StockID" },
                values: new object[,]
                {
                    { 1, 1, 20, 1 },
                    { 2, 1, 40, 2 },
                    { 3, 1, 70, 3 },
                    { 4, 2, 20, 9 }
                });

            migrationBuilder.InsertData(
                table: "GRNs",
                columns: new[] { "GrnID", "DeliveryDate", "OrderDate", "StockUpdated" },
                values: new object[,]
                {
                    { 1, "20/03/2022", "23/02/2022", false },
                    { 2, " ", "24/02/2022", false }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "ReorderLevel", "ReorderQuantity", "StockOnHand", "SupplierID", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Chai", 10, 14, 9, null, 10.0 },
                    { 2, "Syrup", 25, 8, 10, null, 5.0 },
                    { 3, "Cajun Seasoning", 10, 17, 7, null, 7.0 },
                    { 4, "Olive Oil", 10, 16, 8, null, 8.0 },
                    { 5, "Boysenberry Spread", 25, 19, 5, null, 6.0 },
                    { 6, "Dried Pears", 10, 23, 7, null, 6.0 },
                    { 7, "Curry Sauce", 10, 30, 6, null, 9.0 },
                    { 8, "Walnuts", 10, 17, 1, null, 2.0 },
                    { 9, "Fruit Cocktail", 10, 29, 8, null, 7.0 },
                    { 10, "Chocolate Biscuits Mix", 5, 7, 1, null, 2.0 },
                    { 11, "Marmalade", 10, 61, 2, null, 10.0 },
                    { 12, "Scones", 5, 8, 5, null, 6.0 },
                    { 13, "Beer", 15, 11, 4, null, 7.0 },
                    { 14, "Crab Meat", 30, 14, 3, null, 2.0 },
                    { 15, "Clam Chowder", 10, 7, 6, null, 10.0 },
                    { 16, "Coffee", 25, 35, 1, null, 3.0 },
                    { 17, "Chocolate", 25, 10, 1, null, 10.0 },
                    { 18, "Dried Apples", 10, 40, 3, null, 1.0 },
                    { 19, "Long Grain Rice", 25, 5, 3, null, 7.0 },
                    { 20, "Gnocchi", 30, 29, 3, null, 8.0 },
                    { 21, "Ravioli", 20, 15, 7, null, 5.0 },
                    { 22, "Hot Pepper Sauce", 10, 16, 7, null, 6.0 },
                    { 23, "Tomato Sauce", 20, 13, 5, null, 9.0 },
                    { 24, "Mozzarella", 10, 26, 4, null, 9.0 },
                    { 25, "Almonds", 5, 8, 5, null, 9.0 },
                    { 26, "Mustard", 15, 10, 3, null, 4.0 },
                    { 27, "Dried Plums", 50, 3, 3, null, 7.0 },
                    { 28, "Green Tea", 100, 2, 2, null, 3.0 },
                    { 29, "Granola", 20, 2, 2, null, 2.0 },
                    { 30, "Potato Chips", 30, 1, 3, null, 5.0 },
                    { 31, "Brownie Mix", 10, 9, 2, null, 8.0 },
                    { 32, "Cake Mix", 10, 11, 8, null, 7.0 },
                    { 33, "Tea", 20, 2, 3, null, 4.0 },
                    { 34, "Pears", 10, 1, 10, null, 1.0 },
                    { 35, "Peaches", 10, 1, 7, null, 7.0 },
                    { 36, "Pineapple", 10, 1, 1, null, 6.0 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "ReorderLevel", "ReorderQuantity", "StockOnHand", "SupplierID", "UnitPrice" },
                values: new object[,]
                {
                    { 37, "Cherry Pie Filling", 10, 1, 10, null, 2.0 },
                    { 38, "Green Beans", 10, 1, 2, null, 9.0 },
                    { 39, "Corn", 10, 1, 3, null, 6.0 },
                    { 40, "Peas", 10, 1, 3, null, 6.0 },
                    { 41, "Tuna Fish", 30, 1, 8, null, 2.0 },
                    { 42, "Smoked Salmon", 30, 2, 9, null, 10.0 },
                    { 43, "Hot Cereal", 50, 3, 8, null, 6.0 },
                    { 44, "Vegetable Soup", 100, 1, 4, null, 9.0 },
                    { 45, "Chicken Soup", 100, 1, 2, null, 8.0 }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "SupplierID", "Address", "City", "CompanyName", "ContactName", "ContactTitle", "Country", "Phone", "PostalCode", "Region" },
                values: new object[,]
                {
                    { 1, "49 Gilbert St.", "London", "Exotic Liquids", "Charlotte Cooper", "Purchasing Manager", "UK", "(171) 555-2222", "EC1 4SD", "NULL" },
                    { 2, "P.O. Box 78934", "New Orleans", "New Orleans Cajun Delights", "Shelley Burke", "Order Administrator", "USA", "(100) 555-4822", "70117", "LA" },
                    { 3, "707 Oxford Rd.", "Ann Arbor", "Grandma Kelly's Homestead", "Regina Murphy", "Sales Representative", "USA", "(313) 555-5735", "48104", "MI" },
                    { 4, "9-8 Sekimai Musashino-shi", "Tokyo", "Tokyo Traders", "Yoshi Nagase", "Marketing Manager", "Japan", "(03) 3555-5011", "100", "NULL" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierID",
                table: "Products",
                column: "SupplierID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GRNLines");

            migrationBuilder.DropTable(
                name: "GRNs");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
