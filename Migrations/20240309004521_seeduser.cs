using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace me_faz_um_pix.Migrations
{
    /// <inheritdoc />
    public partial class seeduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO ""User"" (""CreatedAt"", ""UpdatedAt"", ""Cpf"", ""Name"")
            VALUES 
            (NOW(), NOW(), '11111111111', 'User1'),
            (NOW(), NOW(), '22222222222', 'User2'),
            (NOW(), NOW(), '33333333333', 'User3'),
            (NOW(), NOW(), '44444444444', 'User4'),
            (NOW(), NOW(), '55555555555', 'User5'),
            (NOW(), NOW(), '66666666666', 'User6'),
            (NOW(), NOW(), '77777777777', 'User7'),
            (NOW(), NOW(), '88888888888', 'User8'),
            (NOW(), NOW(), '99999999999', 'User9'),
            (NOW(), NOW(), '00000000000', 'User10');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"TRUNCATE TABLE ""User"";");
        }
    }
}
