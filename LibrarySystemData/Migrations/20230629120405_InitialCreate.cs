using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystemData.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Surname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Lastname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Email_isConfiormed = table.Column<bool>(type: "bit", nullable: true),
                    Date_ofBirth = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Storage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Author_Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_id = table.Column<int>(type: "int", nullable: true),
                    Autor_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Author_Bo__Autor__5EBF139D",
                        column: x => x.Autor_id,
                        principalTable: "Author",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Author_Bo__Book___5DCAEF64",
                        column: x => x.Book_id,
                        principalTable: "Book",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Book_Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_id = table.Column<int>(type: "int", nullable: true),
                    Genre_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Genre", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Book_Genr__Book___59063A47",
                        column: x => x.Book_id,
                        principalTable: "Book",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Book_Genr__Genre__59FA5E80",
                        column: x => x.Genre_id,
                        principalTable: "Genres",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Book_Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_id = table.Column<int>(type: "int", nullable: true),
                    Languages_id = table.Column<int>(type: "int", nullable: true),
                    NumberOfBook = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Languages", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Book_Lang__Book___19DFD96B",
                        column: x => x.Book_id,
                        principalTable: "Book",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Book_Lang__Langu__1AD3FDA4",
                        column: x => x.Languages_id,
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Borrows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_id = table.Column<int>(type: "int", nullable: true),
                    Person_id = table.Column<int>(type: "int", nullable: true),
                    Take_time = table.Column<DateTime>(type: "date", nullable: true),
                    Returned_time = table.Column<DateTime>(type: "date", nullable: true),
                    Actual_returned_time = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrows", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Borrows__Book_id__72C60C4A",
                        column: x => x.Book_id,
                        principalTable: "Book",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Borrows__Person___71D1E811",
                        column: x => x.Person_id,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Person_id = table.Column<int>(type: "int", nullable: true),
                    Login = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Users__Person_id__3D5E1FD2",
                        column: x => x.Person_id,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Person_Position",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Person_id = table.Column<int>(type: "int", nullable: true),
                    Position_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person_Position", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Person_Po__Perso__6E01572D",
                        column: x => x.Person_id,
                        principalTable: "Person",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Person_Po__Posit__6D0D32F4",
                        column: x => x.Position_id,
                        principalTable: "Position",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Book_Publisher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_id = table.Column<int>(type: "int", nullable: true),
                    Publisher_id = table.Column<int>(type: "int", nullable: true),
                    NumberOfBook = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Publisher", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Book_Publ__Book___5441852A",
                        column: x => x.Book_id,
                        principalTable: "Book",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Book_Publ__Publi__5535A963",
                        column: x => x.Publisher_id,
                        principalTable: "Publisher",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Book_Storage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_id = table.Column<int>(type: "int", nullable: true),
                    Storage_id = table.Column<int>(type: "int", nullable: true),
                    NumberOfBook = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Storage", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Book_Stor__Book___4F7CD00D",
                        column: x => x.Book_id,
                        principalTable: "Book",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Book_Stor__Stora__5070F446",
                        column: x => x.Storage_id,
                        principalTable: "Storage",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Role_Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_id = table.Column<int>(type: "int", nullable: true),
                    Users_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Role_User__Role___68487DD7",
                        column: x => x.Role_id,
                        principalTable: "Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Role_User__Users__693CA210",
                        column: x => x.Users_id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TableLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Table_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TableId = table.Column<int>(type: "int", nullable: true),
                    Create_date = table.Column<DateTime>(type: "date", nullable: true),
                    Change_date = table.Column<DateTime>(type: "date", nullable: true),
                    Delete_date = table.Column<DateTime>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    User_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogsUsers",
                        column: x => x.User_id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Author_Book_Autor_id",
                table: "Author_Book",
                column: "Autor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Author_Book_Book_id",
                table: "Author_Book",
                column: "Book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Genre_Book_id",
                table: "Book_Genre",
                column: "Book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Genre_Genre_id",
                table: "Book_Genre",
                column: "Genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Languages_Book_id",
                table: "Book_Languages",
                column: "Book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Languages_Languages_id",
                table: "Book_Languages",
                column: "Languages_id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Publisher_Book_id",
                table: "Book_Publisher",
                column: "Book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Publisher_Publisher_id",
                table: "Book_Publisher",
                column: "Publisher_id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Storage_Book_id",
                table: "Book_Storage",
                column: "Book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Storage_Storage_id",
                table: "Book_Storage",
                column: "Storage_id");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_Book_id",
                table: "Borrows",
                column: "Book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_Person_id",
                table: "Borrows",
                column: "Person_id");

            migrationBuilder.CreateIndex(
                name: "IX_Person_Position_Person_id",
                table: "Person_Position",
                column: "Person_id");

            migrationBuilder.CreateIndex(
                name: "IX_Person_Position_Position_id",
                table: "Person_Position",
                column: "Position_id");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Users_Role_id",
                table: "Role_Users",
                column: "Role_id");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Users_Users_id",
                table: "Role_Users",
                column: "Users_id");

            migrationBuilder.CreateIndex(
                name: "IX_TableLogs_User_id",
                table: "TableLogs",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Person_id",
                table: "Users",
                column: "Person_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Author_Book");

            migrationBuilder.DropTable(
                name: "Book_Genre");

            migrationBuilder.DropTable(
                name: "Book_Languages");

            migrationBuilder.DropTable(
                name: "Book_Publisher");

            migrationBuilder.DropTable(
                name: "Book_Storage");

            migrationBuilder.DropTable(
                name: "Borrows");

            migrationBuilder.DropTable(
                name: "Person_Position");

            migrationBuilder.DropTable(
                name: "Role_Users");

            migrationBuilder.DropTable(
                name: "TableLogs");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Publisher");

            migrationBuilder.DropTable(
                name: "Storage");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
