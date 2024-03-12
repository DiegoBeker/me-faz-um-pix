using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace me_faz_um_pix.Migrations
{
    /// <inheritdoc />
    public partial class createIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_Cpf",
                table: "User",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PixKey_Value",
                table: "PixKey",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProvider_Token",
                table: "PaymentProvider",
                column: "Token",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Cpf",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_PixKey_Value",
                table: "PixKey");

            migrationBuilder.DropIndex(
                name: "IX_PaymentProvider_Token",
                table: "PaymentProvider");
        }
    }
}
