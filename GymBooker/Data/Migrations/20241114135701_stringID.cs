using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymBooker.Data.Migrations
{
    /// <inheritdoc />
    public partial class stringID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsersGymClasses_AspNetUsers_ApplicationUserId1",
                table: "ApplicationUsersGymClasses");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsersGymClasses_ApplicationUserId1",
                table: "ApplicationUsersGymClasses");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "ApplicationUsersGymClasses");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "ApplicationUsersGymClasses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsersGymClasses_AspNetUsers_ApplicationUserId",
                table: "ApplicationUsersGymClasses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsersGymClasses_AspNetUsers_ApplicationUserId",
                table: "ApplicationUsersGymClasses");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "ApplicationUsersGymClasses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "ApplicationUsersGymClasses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersGymClasses_ApplicationUserId1",
                table: "ApplicationUsersGymClasses",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsersGymClasses_AspNetUsers_ApplicationUserId1",
                table: "ApplicationUsersGymClasses",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
