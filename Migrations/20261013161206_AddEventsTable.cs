using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaceDayAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddEventsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Events_EventID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventOrganisers_EventOrganiserID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_PARTICIPANT_EVENT_Events_EventID",
                table: "PARTICIPANT_EVENT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "EVENT");

            migrationBuilder.RenameIndex(
                name: "IX_Events_EventOrganiserID",
                table: "EVENT",
                newName: "IX_EVENT_EventOrganiserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EVENT",
                table: "EVENT",
                column: "EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_EVENT_EventID",
                table: "Categories",
                column: "EventID",
                principalTable: "EVENT",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EVENT_EventOrganisers_EventOrganiserID",
                table: "EVENT",
                column: "EventOrganiserID",
                principalTable: "EventOrganisers",
                principalColumn: "EventOrganiserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PARTICIPANT_EVENT_EVENT_EventID",
                table: "PARTICIPANT_EVENT",
                column: "EventID",
                principalTable: "EVENT",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_EVENT_EventID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_EVENT_EventOrganisers_EventOrganiserID",
                table: "EVENT");

            migrationBuilder.DropForeignKey(
                name: "FK_PARTICIPANT_EVENT_EVENT_EventID",
                table: "PARTICIPANT_EVENT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EVENT",
                table: "EVENT");

            migrationBuilder.RenameTable(
                name: "EVENT",
                newName: "Events");

            migrationBuilder.RenameIndex(
                name: "IX_EVENT_EventOrganiserID",
                table: "Events",
                newName: "IX_Events_EventOrganiserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Events_EventID",
                table: "Categories",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventOrganisers_EventOrganiserID",
                table: "Events",
                column: "EventOrganiserID",
                principalTable: "EventOrganisers",
                principalColumn: "EventOrganiserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PARTICIPANT_EVENT_Events_EventID",
                table: "PARTICIPANT_EVENT",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
