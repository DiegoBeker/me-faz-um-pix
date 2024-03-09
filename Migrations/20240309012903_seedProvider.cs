using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace me_faz_um_pix.Migrations
{
    /// <inheritdoc />
    public partial class seedProvider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO ""PaymentProvider"" (""CreatedAt"", ""UpdatedAt"", ""Token"")
            VALUES 
            (NOW(), NOW(), '66m05vwhEAeCU6hCHg641H7l5CbJu8F2XxYFPU6JTVpCJMOWbhEEaGLYaySxsM39'),
            (NOW(), NOW(), 'j71O0crtzh3mmjzY8r0Q37EokUZ1mwmj6Yo0vB2MK1DTArq41GAwXiBq90MDSVYa'),
            (NOW(), NOW(), 'ylLOc5UTlhW3YzFA0B4PDfx1iHljAF3Zui94AZgRJuqpQfKziSCdwPbfLrJPjXbW');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"TRUNCATE TABLE ""PaymentProvider"";");
        }
    }
}
