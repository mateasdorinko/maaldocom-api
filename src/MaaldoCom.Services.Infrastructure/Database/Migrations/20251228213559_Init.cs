using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaaldoCom.Services.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Knowledge",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quote = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaAlbums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlFriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaAlbums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaAlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Media_MediaAlbums_MediaAlbumId",
                        column: x => x.MediaAlbumId,
                        principalTable: "MediaAlbums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaAlbumTag",
                columns: table => new
                {
                    MediaAlbumsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaAlbumTag", x => new { x.MediaAlbumsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_MediaAlbumTag_MediaAlbums_MediaAlbumsId",
                        column: x => x.MediaAlbumsId,
                        principalTable: "MediaAlbums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaAlbumTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaTag",
                columns: table => new
                {
                    MediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaTag", x => new { x.MediaId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_MediaTag_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Media_MediaAlbumId",
                table: "Media",
                column: "MediaAlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaAlbumTag_TagsId",
                table: "MediaAlbumTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaTag_TagsId",
                table: "MediaTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Knowledge");

            migrationBuilder.DropTable(
                name: "MediaAlbumTag");

            migrationBuilder.DropTable(
                name: "MediaTag");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "MediaAlbums");
        }
    }
}
