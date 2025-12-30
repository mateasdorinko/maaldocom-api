using MaaldoCom.Services.Infrastructure.Database.Sql;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaaldoCom.Services.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            InitialSeedRunner.InitialDmlDdl(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
