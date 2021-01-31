using Microsoft.EntityFrameworkCore.Migrations;

namespace ContentCreation.Infrastructure.Migrations
{
    public partial class AlterUserProfileImageFieldTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarImageUri",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "HeaderImageUri",
                table: "UserProfiles");

            migrationBuilder.AddColumn<string>(
                name: "AvatarImageUrn",
                table: "UserProfiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderImageUrn",
                table: "UserProfiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarImageUrn",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "HeaderImageUrn",
                table: "UserProfiles");

            migrationBuilder.AddColumn<string>(
                name: "AvatarImageUri",
                table: "UserProfiles",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderImageUri",
                table: "UserProfiles",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
