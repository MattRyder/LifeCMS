using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContentCreation.Infrastructure.Migrations
{
    public partial class CreateCampaigns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    UserId = table.Column<Guid>(nullable: false),
                    NewsletterTemplateId = table.Column<Guid>(nullable: false),
                    UserProfileId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Subject_SubjectLineText = table.Column<string>(nullable: true),
                    Subject_PreviewText = table.Column<string>(nullable: true),
                    ScheduledDate = table.Column<DateTime>(nullable: false),
                    UseSubscriberTimeZone = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Campaigns");
        }
    }
}
