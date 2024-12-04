using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Etn.MyLittleBoard.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class ProjectStatusMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Status",
            table: "Projects",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Status",
            table: "Projects");
    }
}
