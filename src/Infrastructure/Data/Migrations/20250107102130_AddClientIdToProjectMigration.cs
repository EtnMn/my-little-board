using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Etn.MyLittleBoard.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class AddClientIdToProjectMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "ClientId",
            table: "Projects",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "ClientId",
            table: "Projects");
    }
}
