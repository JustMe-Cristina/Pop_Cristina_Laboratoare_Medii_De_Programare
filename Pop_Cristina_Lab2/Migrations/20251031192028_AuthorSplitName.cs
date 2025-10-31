using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pop_Cristina_Lab2.Migrations
{
    /// <inheritdoc />
    public partial class AuthorSplitName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Author",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Author",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Author");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Author",
                newName: "Name");
        }
    }
}
