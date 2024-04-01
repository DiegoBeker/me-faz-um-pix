using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace me_faz_um_pix.Migrations
{
    /// <inheritdoc />
    public partial class indexPpa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PaymentProviderAccount_Agency_Number",
                table: "PaymentProviderAccount",
                columns: new[] { "Agency", "Number" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PaymentProviderAccount_Agency_Number",
                table: "PaymentProviderAccount");
        }
    }
}
