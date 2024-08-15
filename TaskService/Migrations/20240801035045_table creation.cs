using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskService.Migrations
{
    /// <inheritdoc />
    public partial class tablecreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewTask1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    PackagingNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InspectionRating = table.Column<int>(type: "int", nullable: true),
                    SandblastingandSolderingNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SandblastingandSolderingAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaintingNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stepname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaintType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewTask1", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewTask1");
        }
    }
}
