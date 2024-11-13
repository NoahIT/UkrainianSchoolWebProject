using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class dddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentsHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MerchantAccount = table.Column<string>(type: "longtext", nullable: true),
                    OrderReference = table.Column<string>(type: "longtext", nullable: true),
                    MerchantSignature = table.Column<string>(type: "longtext", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "longtext", nullable: true),
                    AuthCode = table.Column<string>(type: "longtext", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    Phone = table.Column<string>(type: "longtext", nullable: true),
                    CreatedDate = table.Column<long>(type: "bigint", nullable: false),
                    ProcessingDate = table.Column<long>(type: "bigint", nullable: false),
                    CardPan = table.Column<string>(type: "longtext", nullable: true),
                    CardType = table.Column<string>(type: "longtext", nullable: true),
                    IssuerBankCountry = table.Column<string>(type: "longtext", nullable: true),
                    IssuerBankName = table.Column<string>(type: "longtext", nullable: true),
                    RecToken = table.Column<string>(type: "longtext", nullable: true),
                    TransactionStatus = table.Column<string>(type: "longtext", nullable: true),
                    Reason = table.Column<string>(type: "longtext", nullable: true),
                    ReasonCode = table.Column<int>(type: "int", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentSystem = table.Column<string>(type: "longtext", nullable: true),
                    AcquirerBankName = table.Column<string>(type: "longtext", nullable: true),
                    CardProduct = table.Column<string>(type: "longtext", nullable: true),
                    ClientName = table.Column<string>(type: "longtext", nullable: true),
                    CreatedHistory = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentsHistories", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentsHistories");
        }
    }
}
