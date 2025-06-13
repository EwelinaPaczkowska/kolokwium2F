using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace kolokwium2F.Migrations
{
    /// <inheritdoc />
    public partial class InitializedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    ArtistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.ArtistId);
                });

            migrationBuilder.CreateTable(
                name: "Artwork",
                columns: table => new
                {
                    ArtworkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    YearCreated = table.Column<int>(type: "int", nullable: false),
                    InsuranceValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artwork", x => x.ArtworkId);
                });

            migrationBuilder.CreateTable(
                name: "Gallery",
                columns: table => new
                {
                    GalleryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EstablishedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gallery", x => x.GalleryId);
                });

            migrationBuilder.CreateTable(
                name: "Exhibition",
                columns: table => new
                {
                    ExhibitionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GalleryId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfArtworks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exhibition", x => x.ExhibitionId);
                    table.ForeignKey(
                        name: "FK_Exhibition_Gallery_GalleryId",
                        column: x => x.GalleryId,
                        principalTable: "Gallery",
                        principalColumn: "GalleryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExhibitionArtwork",
                columns: table => new
                {
                    ExhibitionId = table.Column<int>(type: "int", nullable: false),
                    ArtworkId = table.Column<int>(type: "int", nullable: false),
                    GalleryId = table.Column<int>(type: "int", nullable: false),
                    InsuranceValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExhibitionArtwork", x => x.ExhibitionId);
                    table.ForeignKey(
                        name: "FK_ExhibitionArtwork_Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artwork",
                        principalColumn: "ArtworkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExhibitionArtwork_Exhibition_ExhibitionId",
                        column: x => x.ExhibitionId,
                        principalTable: "Exhibition",
                        principalColumn: "ExhibitionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExhibitionArtwork_Gallery_GalleryId",
                        column: x => x.GalleryId,
                        principalTable: "Gallery",
                        principalColumn: "GalleryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Artist",
                columns: new[] { "ArtistId", "BirthDate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 10, 20, 0, 0, 0, DateTimeKind.Unspecified), "k", "A" },
                    { 2, new DateTime(2025, 7, 15, 19, 30, 0, 0, DateTimeKind.Unspecified), "a", "B" },
                    { 3, new DateTime(2025, 8, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), "b", "C" }
                });

            migrationBuilder.InsertData(
                table: "Artwork",
                columns: new[] { "ArtworkId", "ArtistId", "InsuranceValue", "Title", "YearCreated" },
                values: new object[,]
                {
                    { 1, 0, 0, "AB-001", 2010 },
                    { 2, 0, 0, "AB-002", 2002 },
                    { 3, 0, 0, "CD-001", 2015 },
                    { 4, 0, 0, "EF-001", 2025 }
                });

            migrationBuilder.InsertData(
                table: "Exhibition",
                columns: new[] { "ExhibitionId", "EndDate", "GalleryId", "NumberOfArtworks", "StartDate", "Title" },
                values: new object[] { 4, new DateTime(2025, 7, 10, 20, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, new DateTime(2025, 6, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Aq-001" });

            migrationBuilder.InsertData(
                table: "Gallery",
                columns: new[] { "GalleryId", "EstablishedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 10, 20, 0, 0, 0, DateTimeKind.Unspecified), "A" },
                    { 2, new DateTime(2025, 7, 10, 20, 0, 0, 0, DateTimeKind.Unspecified), "J" },
                    { 3, new DateTime(2025, 7, 10, 20, 0, 0, 0, DateTimeKind.Unspecified), "P" }
                });

            migrationBuilder.InsertData(
                table: "Exhibition",
                columns: new[] { "ExhibitionId", "EndDate", "GalleryId", "NumberOfArtworks", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 10, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, new DateTime(2025, 6, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "AB-001" },
                    { 2, new DateTime(2025, 7, 10, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, new DateTime(2025, 6, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Ac-001" },
                    { 3, new DateTime(2025, 7, 10, 20, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, new DateTime(2025, 6, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "As-001" }
                });

            migrationBuilder.InsertData(
                table: "ExhibitionArtwork",
                columns: new[] { "ExhibitionId", "ArtworkId", "GalleryId", "InsuranceValue" },
                values: new object[,]
                {
                    { 4, 3, 0, 2m },
                    { 1, 1, 0, 2m },
                    { 2, 2, 0, 2m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exhibition_GalleryId",
                table: "Exhibition",
                column: "GalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExhibitionArtwork_ArtworkId",
                table: "ExhibitionArtwork",
                column: "ArtworkId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExhibitionArtwork_GalleryId",
                table: "ExhibitionArtwork",
                column: "GalleryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artist");

            migrationBuilder.DropTable(
                name: "ExhibitionArtwork");

            migrationBuilder.DropTable(
                name: "Artwork");

            migrationBuilder.DropTable(
                name: "Exhibition");

            migrationBuilder.DropTable(
                name: "Gallery");
        }
    }
}
