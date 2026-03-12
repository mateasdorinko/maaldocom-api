using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaaldoCom.Api.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class WritingsDataModelAddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Author = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Writings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Blurb = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Writings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaAlbumComments",
                columns: table => new
                {
                    MediaAlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaAlbumComments", x => new { x.MediaAlbumId, x.CommentId });
                    table.ForeignKey(
                        name: "FK_MediaAlbumComments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaAlbumComments_MediaAlbums_MediaAlbumId",
                        column: x => x.MediaAlbumId,
                        principalTable: "MediaAlbums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaComments",
                columns: table => new
                {
                    MediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaComments", x => new { x.MediaId, x.CommentId });
                    table.ForeignKey(
                        name: "FK_MediaComments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaComments_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WritingComments",
                columns: table => new
                {
                    WritingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WritingComments", x => new { x.WritingId, x.CommentId });
                    table.ForeignKey(
                        name: "FK_WritingComments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WritingComments_Writings_WritingId",
                        column: x => x.WritingId,
                        principalTable: "Writings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WritingTags",
                columns: table => new
                {
                    WritingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WritingTags", x => new { x.WritingId, x.TagId });
                    table.ForeignKey(
                        name: "FK_WritingTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WritingTags_Writings_WritingId",
                        column: x => x.WritingId,
                        principalTable: "Writings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaAlbumComments_CommentId",
                table: "MediaAlbumComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaComments_CommentId",
                table: "MediaComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_WritingComments_CommentId",
                table: "WritingComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Writings_Slug",
                table: "Writings",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Writings_Title",
                table: "Writings",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WritingTags_TagId",
                table: "WritingTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaAlbumComments");

            migrationBuilder.DropTable(
                name: "MediaComments");

            migrationBuilder.DropTable(
                name: "WritingComments");

            migrationBuilder.DropTable(
                name: "WritingTags");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Writings");
        }
    }
}
