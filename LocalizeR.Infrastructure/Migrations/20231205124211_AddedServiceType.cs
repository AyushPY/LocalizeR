﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalizeR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedServiceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceType",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "AspNetUsers");
        }
    }
}
