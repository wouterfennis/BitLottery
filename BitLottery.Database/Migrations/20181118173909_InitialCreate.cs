using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BitLottery.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shared");

            migrationBuilder.CreateSequence<int>(
                name: "CustomerNumbers",
                schema: "shared",
                startValue: 1000L,
                incrementBy: 2);

            migrationBuilder.CreateSequence<int>(
                name: "DrawNumbers",
                schema: "shared",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR shared.CustomerNumbers"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Draws",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR shared.DrawNumbers"),
                    SellUntilDate = table.Column<DateTime>(nullable: false),
                    DrawDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Draws", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PriceType = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    DrawId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prices_Draws_DrawId",
                        column: x => x.DrawId,
                        principalTable: "Draws",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ballots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SellDate = table.Column<DateTime>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    WonPriceId = table.Column<int>(nullable: true),
                    CustomerId = table.Column<int>(nullable: true),
                    DrawId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ballots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ballots_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ballots_Draws_DrawId",
                        column: x => x.DrawId,
                        principalTable: "Draws",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ballots_Prices_WonPriceId",
                        column: x => x.WonPriceId,
                        principalTable: "Prices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ballots_CustomerId",
                table: "Ballots",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ballots_DrawId",
                table: "Ballots",
                column: "DrawId");

            migrationBuilder.CreateIndex(
                name: "IX_Ballots_WonPriceId",
                table: "Ballots",
                column: "WonPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_DrawId",
                table: "Prices",
                column: "DrawId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ballots");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Draws");

            migrationBuilder.DropSequence(
                name: "CustomerNumbers",
                schema: "shared");

            migrationBuilder.DropSequence(
                name: "DrawNumbers",
                schema: "shared");
        }
    }
}
