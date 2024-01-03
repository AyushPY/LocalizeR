using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalizeR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RequestDetailsColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestDetails",
                table: "RequestRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestDetails",
                table: "RequestRecords");
        }
    }
}
