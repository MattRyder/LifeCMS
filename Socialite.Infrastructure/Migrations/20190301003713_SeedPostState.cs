using Microsoft.EntityFrameworkCore.Migrations;

namespace Socialite.Infrastructure.Migrations
{
    public partial class SeedPostState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostState_StateId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostState",
                table: "PostState");

            migrationBuilder.RenameTable(
                name: "PostState",
                newName: "PostStates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostStates",
                table: "PostStates",
                column: "Id");

            migrationBuilder.InsertData(
                table: "PostStates",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "drafted" });

            migrationBuilder.InsertData(
                table: "PostStates",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "published" });

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostStates_StateId",
                table: "Posts",
                column: "StateId",
                principalTable: "PostStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostStates_StateId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostStates",
                table: "PostStates");

            migrationBuilder.DeleteData(
                table: "PostStates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PostStates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "PostStates",
                newName: "PostState");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostState",
                table: "PostState",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostState_StateId",
                table: "Posts",
                column: "StateId",
                principalTable: "PostState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
