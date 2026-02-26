using MaaldoCom.Api.Infrastructure.Database.Seeding;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaaldoCom.Api.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            InitSeeder.Up(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            InitSeeder.Down(migrationBuilder);
        }
    }
}
