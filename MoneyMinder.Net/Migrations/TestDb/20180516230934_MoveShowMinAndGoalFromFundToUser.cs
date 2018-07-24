using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyMinder.Net.Migrations.TestDb
{
    public partial class MoveShowMinAndGoalFromFundToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowMinAndGoal",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "UserTotal",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "ShowMinAndGoal",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowMinAndGoal",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "ShowMinAndGoal",
                table: "Funds",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "UserTotal",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
