using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaaldoCom.Api.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class UrlFriendlyNameRenameToSlug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrlFriendlyName",
                table: "MediaAlbums",
                newName: "Slug");

            migrationBuilder.RenameIndex(
                name: "IX_MediaAlbums_UrlFriendlyName",
                table: "MediaAlbums",
                newName: "IX_MediaAlbums_Slug");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "MediaAlbums",
                newName: "UrlFriendlyName");

            migrationBuilder.RenameIndex(
                name: "IX_MediaAlbums_Slug",
                table: "MediaAlbums",
                newName: "IX_MediaAlbums_UrlFriendlyName");
        }
    }
}
