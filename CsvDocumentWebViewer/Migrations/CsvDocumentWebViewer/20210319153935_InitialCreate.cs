using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CsvDocumentWebViewer.Migrations.CsvDocumentWebViewer
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientView",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientView", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "ManagerView",
                columns: table => new
                {
                    ManagerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Post = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerView", x => x.ManagerId);
                });

            migrationBuilder.CreateTable(
                name: "ProductView",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductView", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "SalesView",
                columns: table => new
                {
                    SalesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaleCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesView", x => x.SalesId);
                    table.ForeignKey(
                        name: "FK_SalesView_ClientView_ClientId",
                        column: x => x.ClientId,
                        principalTable: "ClientView",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesView_ManagerView_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "ManagerView",
                        principalColumn: "ManagerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesView_ProductView_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductView",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesView_ClientId",
                table: "SalesView",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesView_ManagerId",
                table: "SalesView",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesView_ProductId",
                table: "SalesView",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesView");

            migrationBuilder.DropTable(
                name: "ClientView");

            migrationBuilder.DropTable(
                name: "ManagerView");

            migrationBuilder.DropTable(
                name: "ProductView");
        }
    }
}
