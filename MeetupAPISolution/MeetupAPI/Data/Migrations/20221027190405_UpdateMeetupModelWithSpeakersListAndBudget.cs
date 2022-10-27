using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupAPI.Data.Migrations
{
    public partial class UpdateMeetupModelWithSpeakersListAndBudget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Speaker",
                table: "MeetupModels");

            migrationBuilder.AddColumn<decimal>(
                name: "Budget",
                table: "MeetupModels",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "SpeakerModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeetupModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpeakerModel_MeetupModels_MeetupModelId",
                        column: x => x.MeetupModelId,
                        principalTable: "MeetupModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerModel_MeetupModelId",
                table: "SpeakerModel",
                column: "MeetupModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpeakerModel");

            migrationBuilder.DropColumn(
                name: "Budget",
                table: "MeetupModels");

            migrationBuilder.AddColumn<string>(
                name: "Speaker",
                table: "MeetupModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
