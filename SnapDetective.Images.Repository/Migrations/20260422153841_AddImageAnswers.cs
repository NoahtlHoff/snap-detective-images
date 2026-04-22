using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnapDetective.Images.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddImageAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "Images",
                newName: "Answers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Answers",
                table: "Images",
                newName: "Answer");
        }
    }
}
