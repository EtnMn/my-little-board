using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Etn.MyLittleBoard.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class AddClientMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Color",
            table: "Projects",
            type: "nvarchar(30)",
            maxLength: 30,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(300)",
            oldMaxLength: 300);

        migrationBuilder.CreateTable(
            name: "Client",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                Note = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                State = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Client", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Client");

        migrationBuilder.AlterColumn<string>(
            name: "Color",
            table: "Projects",
            type: "nvarchar(300)",
            maxLength: 300,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(30)",
            oldMaxLength: 30);
    }
}
