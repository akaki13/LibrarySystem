using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystemData.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Book_Genr__Book___59063A47",
                table: "Book_Genre");

            migrationBuilder.DropForeignKey(
                name: "FK__Book_Genr__Genre__59FA5E80",
                table: "Book_Genre");

            migrationBuilder.DropForeignKey(
                name: "FK__Book_Lang__Book___19DFD96B",
                table: "Book_Languages");

            migrationBuilder.DropForeignKey(
                name: "FK__Book_Lang__Langu__1AD3FDA4",
                table: "Book_Languages");

            migrationBuilder.DropForeignKey(
                name: "FK__Book_Publ__Book___5441852A",
                table: "Book_Publisher");

            migrationBuilder.DropForeignKey(
                name: "FK__Book_Publ__Publi__5535A963",
                table: "Book_Publisher");

            migrationBuilder.DropForeignKey(
                name: "FK__Book_Stor__Book___4F7CD00D",
                table: "Book_Storage");

            migrationBuilder.DropForeignKey(
                name: "FK__Book_Stor__Stora__5070F446",
                table: "Book_Storage");

            migrationBuilder.AlterColumn<int>(
                name: "Storage_id",
                table: "Book_Storage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Book_id",
                table: "Book_Storage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Publisher_id",
                table: "Book_Publisher",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Book_id",
                table: "Book_Publisher",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Languages_id",
                table: "Book_Languages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Book_id",
                table: "Book_Languages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Genre_id",
                table: "Book_Genre",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Book_id",
                table: "Book_Genre",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Slider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slider", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK__Book_Genr__Book___59063A47",
                table: "Book_Genre",
                column: "Book_id",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Book_Genr__Genre__59FA5E80",
                table: "Book_Genre",
                column: "Genre_id",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Book_Lang__Book___19DFD96B",
                table: "Book_Languages",
                column: "Book_id",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Book_Lang__Langu__1AD3FDA4",
                table: "Book_Languages",
                column: "Languages_id",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Book_Publ__Book___5441852A",
                table: "Book_Publisher",
                column: "Book_id",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Book_Publ__Publi__5535A963",
                table: "Book_Publisher",
                column: "Publisher_id",
                principalTable: "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Book_Stor__Book___4F7CD00D",
                table: "Book_Storage",
                column: "Book_id",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Book_Stor__Stora__5070F446",
                table: "Book_Storage",
                column: "Storage_id",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Book_Genr__Book___59063A47",
                table: "Book_Genre");

            migrationBuilder.DropForeignKey(
                name: "FK__Book_Genr__Genre__59FA5E80",
                table: "Book_Genre");

            migrationBuilder.DropForeignKey(
                name: "FK__Book_Lang__Book___19DFD96B",
                table: "Book_Languages");

            migrationBuilder.DropForeignKey(
                name: "FK__Book_Lang__Langu__1AD3FDA4",
                table: "Book_Languages");

            migrationBuilder.DropForeignKey(
                name: "FK__Book_Publ__Book___5441852A",
                table: "Book_Publisher");

            migrationBuilder.DropForeignKey(
                name: "FK__Book_Publ__Publi__5535A963",
                table: "Book_Publisher");

            migrationBuilder.DropForeignKey(
                name: "FK__Book_Stor__Book___4F7CD00D",
                table: "Book_Storage");

            migrationBuilder.DropForeignKey(
                name: "FK__Book_Stor__Stora__5070F446",
                table: "Book_Storage");

            migrationBuilder.DropTable(
                name: "Slider");

            migrationBuilder.AlterColumn<int>(
                name: "Storage_id",
                table: "Book_Storage",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Book_id",
                table: "Book_Storage",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Publisher_id",
                table: "Book_Publisher",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Book_id",
                table: "Book_Publisher",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Languages_id",
                table: "Book_Languages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Book_id",
                table: "Book_Languages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Genre_id",
                table: "Book_Genre",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Book_id",
                table: "Book_Genre",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK__Book_Genr__Book___59063A47",
                table: "Book_Genre",
                column: "Book_id",
                principalTable: "Book",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Book_Genr__Genre__59FA5E80",
                table: "Book_Genre",
                column: "Genre_id",
                principalTable: "Genres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Book_Lang__Book___19DFD96B",
                table: "Book_Languages",
                column: "Book_id",
                principalTable: "Book",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Book_Lang__Langu__1AD3FDA4",
                table: "Book_Languages",
                column: "Languages_id",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Book_Publ__Book___5441852A",
                table: "Book_Publisher",
                column: "Book_id",
                principalTable: "Book",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Book_Publ__Publi__5535A963",
                table: "Book_Publisher",
                column: "Publisher_id",
                principalTable: "Publisher",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Book_Stor__Book___4F7CD00D",
                table: "Book_Storage",
                column: "Book_id",
                principalTable: "Book",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Book_Stor__Stora__5070F446",
                table: "Book_Storage",
                column: "Storage_id",
                principalTable: "Storage",
                principalColumn: "Id");
        }
    }
}
