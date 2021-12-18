using Microsoft.EntityFrameworkCore.Migrations;

namespace CoursesProject.Migrations
{
    public partial class UpdateDataBaseStru : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technologies_Courses_CourseId",
                table: "Technologies");

            migrationBuilder.DropIndex(
                name: "IX_Technologies_CourseId",
                table: "Technologies");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Technologies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Technologies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Technologies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Trainers",
                newName: "TrainerName");

            migrationBuilder.RenameColumn(
                name: "TechnologyId",
                table: "Courses",
                newName: "CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CourseName",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryId",
                table: "Courses",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Technologies_CategoryId",
                table: "Courses",
                column: "CategoryId",
                principalTable: "Technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Technologies_CategoryId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CategoryId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "TrainerName",
                table: "Trainers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Courses",
                newName: "TechnologyId");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Technologies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Fadi", "Hboubati" },
                    { 2, "Taghrid", "Alshoqirat" },
                    { 3, "Ahmad", "Ali" },
                    { 4, "Baraa", "Al-Hamad" }
                });

            migrationBuilder.InsertData(
                table: "Technologies",
                columns: new[] { "Id", "CourseId", "Name" },
                values: new object[,]
                {
                    { 1, null, "python" },
                    { 2, null, "JavaScript" },
                    { 3, null, "CSharp" }
                });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "Id", "Name", "attachment" },
                values: new object[] { 1, "Ahmad", "Not-Found" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Name", "TechnologyId", "TrainerId" },
                values: new object[] { 1, "401d5", 3, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_CourseId",
                table: "Technologies",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technologies_Courses_CourseId",
                table: "Technologies",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
