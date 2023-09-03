using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystemData.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Borrows__Book_id__72C60C4A",
                table: "Borrows");

            migrationBuilder.DropForeignKey(
                name: "FK__Borrows__Person___71D1E811",
                table: "Borrows");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Slider",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Slider",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Take_time",
                table: "Borrows",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Returned_time",
                table: "Borrows",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Person_id",
                table: "Borrows",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Book_id",
                table: "Borrows",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Borrows",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Borrows__Book_id__72C60C4A",
                table: "Borrows",
                column: "Book_id",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Borrows__Person___71D1E811",
                table: "Borrows",
                column: "Person_id",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Borrows__Book_id__72C60C4A",
                table: "Borrows");

            migrationBuilder.DropForeignKey(
                name: "FK__Borrows__Person___71D1E811",
                table: "Borrows");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Slider");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Slider");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Borrows");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Take_time",
                table: "Borrows",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Returned_time",
                table: "Borrows",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "Person_id",
                table: "Borrows",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Book_id",
                table: "Borrows",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK__Borrows__Book_id__72C60C4A",
                table: "Borrows",
                column: "Book_id",
                principalTable: "Book",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Borrows__Person___71D1E811",
                table: "Borrows",
                column: "Person_id",
                principalTable: "Person",
                principalColumn: "Id");
        }
    }
}
