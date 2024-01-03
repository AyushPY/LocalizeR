using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalizeR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CascadeIssueAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestRecords_AspNetUsers_RequesterId",
                table: "RequestRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestRecords_AspNetUsers_ServiceId",
                table: "RequestRecords");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestRecords_AspNetUsers_RequesterId",
                table: "RequestRecords",
                column: "RequesterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestRecords_AspNetUsers_ServiceId",
                table: "RequestRecords",
                column: "ServiceId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestRecords_AspNetUsers_RequesterId",
                table: "RequestRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestRecords_AspNetUsers_ServiceId",
                table: "RequestRecords");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestRecords_AspNetUsers_RequesterId",
                table: "RequestRecords",
                column: "RequesterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestRecords_AspNetUsers_ServiceId",
                table: "RequestRecords",
                column: "ServiceId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
