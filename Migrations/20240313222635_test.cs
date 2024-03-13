using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace me_faz_um_pix.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"
            CREATE OR REPLACE FUNCTION UpdateTimestamps()
            RETURNS TRIGGER AS $$
            BEGIN
                NEW.""UpdatedAt"" = NOW();
                IF TG_OP = 'INSERT' THEN
                    NEW.""CreatedAt"" = NOW();
                END IF;
                RETURN NEW;
            END;
            $$ LANGUAGE plpgsql;

            DO $$
            DECLARE
                tableName TEXT;
            BEGIN
                FOR tableName IN SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' AND table_type = 'BASE TABLE' AND table_name != '__EFMigrationsHistory' LOOP
                    EXECUTE 'CREATE TRIGGER TriggerUpdateTimestamps
                        BEFORE INSERT OR UPDATE ON ""' || tableName || '""
                        FOR EACH ROW
                        EXECUTE FUNCTION UpdateTimestamps();';
                END LOOP;
            END;
            $$;
        ";


            migrationBuilder.Sql(sql);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
