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
            (NOW(), NOW(), 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwianRpIjoiNjlkMTFjN2YtYTMxZS00Y2FjLWIzNzgtNDdkYmMxY2M0YWUwIiwiaXNzIjoibWUtZmF6LXVtLXBpeCIsImF1ZCI6Im1lLWZhei11bS1waXgifQ.AgtHBlheLrx1YsZe4p_ixKJy-XdjNwmx8KBJuiEIhB4'),
            (NOW(), NOW(), 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIyIiwianRpIjoiY2MzNzM1NzYtZDRhMy00ZjdiLTllNmQtYzk0ZmIyNThiNzg3IiwiaXNzIjoibWUtZmF6LXVtLXBpeCIsImF1ZCI6Im1lLWZhei11bS1waXgifQ._akZB8Zn3xrbC9ZrQONe21isogVJ1uVbb9EOQIB5dq0'),
            (NOW(), NOW(), 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIzIiwianRpIjoiNWEzMzAyMmMtNmUxMC00OWU5LWIwY2MtZDUyNjlmY2ZlMjJmIiwiaXNzIjoibWUtZmF6LXVtLXBpeCIsImF1ZCI6Im1lLWZhei11bS1waXgifQ.9S7nr1sffB0jVZ6mSp3G6GJVIC-DGlp8gbbp9VZVLco');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"TRUNCATE TABLE ""PaymentProvider"";");
        }
    }
}
