using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ApplicationDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
        // Create the trigger for updating SearchText if it does not exist
        dbContext.Database.ExecuteSqlRaw(@"
            CREATE OR REPLACE FUNCTION update_search_text() RETURNS TRIGGER AS $$
            BEGIN
                NEW.""search_text"" := NEW.""title"" || ' ' || NEW.""description"" || ' ' || (
                    SELECT ""name"" FROM ""Product"".""categories"" WHERE ""id"" = NEW.""category_id""
                );
                RETURN NEW;
            END;
            $$ LANGUAGE plpgsql;

            DO $$
            BEGIN
                IF NOT EXISTS (SELECT 1 FROM pg_trigger WHERE tgname = 'update_search_text_trigger') THEN
                    CREATE TRIGGER update_search_text_trigger
                    BEFORE INSERT OR UPDATE ON ""Product"".""products""
                    FOR EACH ROW EXECUTE FUNCTION update_search_text();
                END IF;
            END;
            $$;
        ");
    }
}
