using Microsoft.EntityFrameworkCore.Migrations;

namespace CoursesProject.Migrations
{
    public partial class UpdateTrainerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Major",
                table: "Trainers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
