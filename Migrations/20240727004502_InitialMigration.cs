using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TopUpAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopUpBeneficiaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopUpBeneficiaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TopUpOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopUpOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersTopUpBeneficiaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TopUpBeneficiaryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTopUpBeneficiaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersTopUpBeneficiaries_TopUpBeneficiaries_TopUpBeneficiaryId",
                        column: x => x.TopUpBeneficiaryId,
                        principalTable: "TopUpBeneficiaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersTopUpBeneficiaries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopUpAmount = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    UsersTopUpBeneficiariesId = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_UsersTopUpBeneficiaries_UsersTopUpBeneficiariesId",
                        column: x => x.UsersTopUpBeneficiariesId,
                        principalTable: "UsersTopUpBeneficiaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TopUpBeneficiaries",
                columns: new[] { "Id", "Nickname" },
                values: new object[,]
                {
                    { 1, "Beneficiary1" },
                    { 2, "Beneficiary2" },
                    { 3, "Beneficiary3" },
                    { 4, "Beneficiary4" }
                });

            migrationBuilder.InsertData(
                table: "TopUpOptions",
                columns: new[] { "Id", "Amount" },
                values: new object[,]
                {
                    { 1, 5.00m },
                    { 2, 10.00m },
                    { 3, 20.00m },
                    { 4, 30.00m },
                    { 5, 50.00m },
                    { 6, 75.00m },
                    { 7, 100.00m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsVerified", "Name" },
                values: new object[,]
                {
                    { 1, "user1@example.com", true, "User1" },
                    { 2, "user2@example.com", false, "User2" },
                    { 3, "user3@example.com", true, "User3" },
                    { 4, "user4@example.com", false, "User4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UsersTopUpBeneficiariesId",
                table: "Transactions",
                column: "UsersTopUpBeneficiariesId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersTopUpBeneficiaries_TopUpBeneficiaryId",
                table: "UsersTopUpBeneficiaries",
                column: "TopUpBeneficiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersTopUpBeneficiaries_UserId",
                table: "UsersTopUpBeneficiaries",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopUpOptions");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UsersTopUpBeneficiaries");

            migrationBuilder.DropTable(
                name: "TopUpBeneficiaries");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
