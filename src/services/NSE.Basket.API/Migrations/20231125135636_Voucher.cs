using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSE.Basket.API.Migrations
{
    /// <inheritdoc />
    public partial class Voucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "BasketCustomers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "VoucherCode",
                table: "BasketCustomers",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VoucherDiscountType",
                table: "BasketCustomers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VoucherPercent",
                table: "BasketCustomers",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "VoucherUsed",
                table: "BasketCustomers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "VoucherValue",
                table: "BasketCustomers",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "BasketCustomers");

            migrationBuilder.DropColumn(
                name: "VoucherCode",
                table: "BasketCustomers");

            migrationBuilder.DropColumn(
                name: "VoucherDiscountType",
                table: "BasketCustomers");

            migrationBuilder.DropColumn(
                name: "VoucherPercent",
                table: "BasketCustomers");

            migrationBuilder.DropColumn(
                name: "VoucherUsed",
                table: "BasketCustomers");

            migrationBuilder.DropColumn(
                name: "VoucherValue",
                table: "BasketCustomers");
        }
    }
}
