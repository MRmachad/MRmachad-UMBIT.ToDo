using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMBIT.ToDo.API.Migrations
{
    /// <inheritdoc />
    public partial class otherprops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EhRealizada",
                table: "ToDoItem");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "ToDoItem",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ToDoItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "ToDoItem");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ToDoItem");

            migrationBuilder.AddColumn<bool>(
                name: "EhRealizada",
                table: "ToDoItem",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
