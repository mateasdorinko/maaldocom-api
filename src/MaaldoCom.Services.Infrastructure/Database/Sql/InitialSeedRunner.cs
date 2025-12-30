using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MaaldoCom.Services.Infrastructure.Database.Sql;

public static class InitialSeedRunner
    {
        private static string GetSqlFromSqlResource(string sqlResource)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(sqlResource);

            if (stream == null) { throw new ApplicationException($"Stream {sqlResource} is null."); }

            var streamLength = (int)stream.Length;
            var data = new byte[streamLength];
            stream.ReadExactly(data, 0, streamLength);

            // lets remove the UTF8 file header if there is one:
            if ((data[0] != 0xEF) || (data[1] != 0xBB) || (data[2] != 0xBF))
            {
                return Encoding.UTF8.GetString(data);
            }

            var scrubbedData = new byte[data.Length - 3];
            Array.Copy(data, 3, scrubbedData, 0, scrubbedData.Length);
            data = scrubbedData;

            return Encoding.UTF8.GetString(data);
        }

        public static void InitialDmlDdl(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(GetSqlFromSqlResource("MaaldoCom.Services.Infrastructure.Database.Sql.Seeding.InsertKnowledge.sql"));
            //migrationBuilder.Sql(GetSqlFromSqlResource("MaaldoCom.Services.Infrastructure.Database.Sql.Seeding.InsertTags.sql"));
        }
    }