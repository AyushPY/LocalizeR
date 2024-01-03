using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalizeR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RequestSequenceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    budget = table.Column<int>(type: "int", nullable: false),
                    deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestRecords_AspNetUsers_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestRecords_AspNetUsers_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestRecords_RequesterId",
                table: "RequestRecords",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRecords_ServiceId",
                table: "RequestRecords",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestRecords");
        }
    }
}
