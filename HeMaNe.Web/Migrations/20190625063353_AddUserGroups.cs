using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HeMaNe.Web.Migrations
{
    public partial class AddUserGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_User_ManagerId",
                table: "Clubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.DropIndex(
                "IX_User_Username", 
                "Users");

            migrationBuilder.CreateIndex(
                "IX_Users_Username", 
                "Users", 
                "Username");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sports",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTimeOffset",
                table: "Days",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "Days",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Group",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Days_LeagueId",
                table: "Days",
                column: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_Users_ManagerId",
                table: "Clubs",
                column: "ManagerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Leagues_LeagueId",
                table: "Days",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_Users_ManagerId",
                table: "Clubs");

            migrationBuilder.DropForeignKey(
                name: "FK_Days_Leagues_LeagueId",
                table: "Days");

            migrationBuilder.DropIndex(
                name: "IX_Days_LeagueId",
                table: "Days");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sports");

            migrationBuilder.DropColumn(
                name: "DateTimeOffset",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Username",
                table: "User",
                newName: "IX_User_Username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_User_ManagerId",
                table: "Clubs",
                column: "ManagerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
