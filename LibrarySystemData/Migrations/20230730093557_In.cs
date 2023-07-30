using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystemData.Migrations
{
    public partial class In : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Book",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Book",
                type: "varchar(300)",
                unicode: false,
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldUnicode: false,
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Book",
                type: "varchar(300)",
                unicode: false,
                maxLength: 300,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ByPopularity");

            migrationBuilder.DropTable(
                name: "ClientsPerformance");

            migrationBuilder.DropTable(
                name: "CurrentTransactions");

            migrationBuilder.DropTable(
                name: "OverdueTransactions");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Book");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Book",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Book",
                type: "varchar(150)",
                unicode: false,
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldUnicode: false,
                oldMaxLength: 300,
                oldNullable: true);
        }
    }
}
