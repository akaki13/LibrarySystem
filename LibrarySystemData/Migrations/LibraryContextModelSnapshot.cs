﻿// <auto-generated />
using System;
using LibrarySystemData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibrarySystemData.Migrations
{
    [DbContext(typeof(LibraryContext))]
    partial class LibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LibrarySystemModels.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Surname")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Author", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModels.AuthorBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AutorId")
                        .HasColumnType("int")
                        .HasColumnName("Autor_id");

                    b.Property<int?>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("Book_id");

                    b.HasKey("Id");

                    b.HasIndex("AutorId");

                    b.HasIndex("BookId");

                    b.ToTable("Author_Book", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModels.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Book", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModels.BookGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("Book_id");

                    b.Property<int?>("GenreId")
                        .HasColumnType("int")
                        .HasColumnName("Genre_id");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("GenreId");

                    b.ToTable("Book_Genre", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModels.BookLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("Book_id");

                    b.Property<int?>("LanguagesId")
                        .HasColumnType("int")
                        .HasColumnName("Languages_id");

                    b.Property<int?>("NumberOfBook")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("LanguagesId");

                    b.ToTable("Book_Languages", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModels.BookPublisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("Book_id");

                    b.Property<int?>("NumberOfBook")
                        .HasColumnType("int");

                    b.Property<int?>("PublisherId")
                        .HasColumnType("int")
                        .HasColumnName("Publisher_id");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Book_Publisher", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModels.BookStorage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("Book_id");

                    b.Property<int?>("NumberOfBook")
                        .HasColumnType("int");

                    b.Property<int?>("StorageId")
                        .HasColumnType("int")
                        .HasColumnName("Storage_id");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("StorageId");

                    b.ToTable("Book_Storage", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModels.Borrow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("ActualReturnedTime")
                        .HasColumnType("date")
                        .HasColumnName("Actual_returned_time");

                    b.Property<int?>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("Book_id");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int")
                        .HasColumnName("Person_id");

                    b.Property<DateTime?>("ReturnedTime")
                        .HasColumnType("date")
                        .HasColumnName("Returned_time");

                    b.Property<DateTime?>("TakeTime")
                        .HasColumnType("date")
                        .HasColumnName("Take_time");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("PersonId");

                    b.ToTable("Borrows");
                });

            modelBuilder.Entity("LibrarySystemModels.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("LibrarySystemModels.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("LibrarySystemModels.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("Date_ofBirth");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<bool?>("EmailIsConfiormed")
                        .HasColumnType("bit")
                        .HasColumnName("Email_isConfiormed");

                    b.Property<string>("Firstname")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Lastname")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Person", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModels.PersonPosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("PersonId")
                        .HasColumnType("int")
                        .HasColumnName("Person_id");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int")
                        .HasColumnName("Position_id");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("PositionId");

                    b.ToTable("Person_Position", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModels.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Position", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModels.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Publisher", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModels.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModels.RoleUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("Role_id");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int")
                        .HasColumnName("Users_id");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UsersId");

                    b.ToTable("Role_Users", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModels.Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Storage", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModels.TableLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("ChangeDate")
                        .HasColumnType("date")
                        .HasColumnName("Change_date");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("date")
                        .HasColumnName("Create_date");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("date")
                        .HasColumnName("Delete_date");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("TableId")
                        .HasColumnType("int");

                    b.Property<string>("TableName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Table_name");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("User_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TableLogs");
                });

            modelBuilder.Entity("LibrarySystemModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Login")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int")
                        .HasColumnName("Person_id");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LibrarySystemModels.AuthorBook", b =>
                {
                    b.HasOne("LibrarySystemModels.Author", "Autor")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("AutorId")
                        .HasConstraintName("FK__Author_Bo__Autor__5EBF139D");

                    b.HasOne("LibrarySystemModels.Book", "Book")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK__Author_Bo__Book___5DCAEF64");

                    b.Navigation("Autor");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LibrarySystemModels.BookGenre", b =>
                {
                    b.HasOne("LibrarySystemModels.Book", "Book")
                        .WithMany("BookGenres")
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK__Book_Genr__Book___59063A47");

                    b.HasOne("LibrarySystemModels.Genre", "Genre")
                        .WithMany("BookGenres")
                        .HasForeignKey("GenreId")
                        .HasConstraintName("FK__Book_Genr__Genre__59FA5E80");

                    b.Navigation("Book");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("LibrarySystemModels.BookLanguage", b =>
                {
                    b.HasOne("LibrarySystemModels.Book", "Book")
                        .WithMany("BookLanguages")
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK__Book_Lang__Book___19DFD96B");

                    b.HasOne("LibrarySystemModels.Language", "Languages")
                        .WithMany("BookLanguages")
                        .HasForeignKey("LanguagesId")
                        .HasConstraintName("FK__Book_Lang__Langu__1AD3FDA4");

                    b.Navigation("Book");

                    b.Navigation("Languages");
                });

            modelBuilder.Entity("LibrarySystemModels.BookPublisher", b =>
                {
                    b.HasOne("LibrarySystemModels.Book", "Book")
                        .WithMany("BookPublishers")
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK__Book_Publ__Book___5441852A");

                    b.HasOne("LibrarySystemModels.Publisher", "Publisher")
                        .WithMany("BookPublishers")
                        .HasForeignKey("PublisherId")
                        .HasConstraintName("FK__Book_Publ__Publi__5535A963");

                    b.Navigation("Book");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("LibrarySystemModels.BookStorage", b =>
                {
                    b.HasOne("LibrarySystemModels.Book", "Book")
                        .WithMany("BookStorages")
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK__Book_Stor__Book___4F7CD00D");

                    b.HasOne("LibrarySystemModels.Storage", "Storage")
                        .WithMany("BookStorages")
                        .HasForeignKey("StorageId")
                        .HasConstraintName("FK__Book_Stor__Stora__5070F446");

                    b.Navigation("Book");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("LibrarySystemModels.Borrow", b =>
                {
                    b.HasOne("LibrarySystemModels.Book", "Book")
                        .WithMany("Borrows")
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK__Borrows__Book_id__72C60C4A");

                    b.HasOne("LibrarySystemModels.Person", "Person")
                        .WithMany("Borrows")
                        .HasForeignKey("PersonId")
                        .HasConstraintName("FK__Borrows__Person___71D1E811");

                    b.Navigation("Book");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("LibrarySystemModels.PersonPosition", b =>
                {
                    b.HasOne("LibrarySystemModels.Person", "Person")
                        .WithMany("PersonPositions")
                        .HasForeignKey("PersonId")
                        .HasConstraintName("FK__Person_Po__Perso__6E01572D");

                    b.HasOne("LibrarySystemModels.Position", "Position")
                        .WithMany("PersonPositions")
                        .HasForeignKey("PositionId")
                        .HasConstraintName("FK__Person_Po__Posit__6D0D32F4");

                    b.Navigation("Person");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("LibrarySystemModels.RoleUser", b =>
                {
                    b.HasOne("LibrarySystemModels.Role", "Role")
                        .WithMany("RoleUsers")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__Role_User__Role___68487DD7");

                    b.HasOne("LibrarySystemModels.User", "Users")
                        .WithMany("RoleUsers")
                        .HasForeignKey("UsersId")
                        .HasConstraintName("FK__Role_User__Users__693CA210");

                    b.Navigation("Role");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("LibrarySystemModels.TableLog", b =>
                {
                    b.HasOne("LibrarySystemModels.User", "User")
                        .WithMany("TableLogs")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_LogsUsers");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibrarySystemModels.User", b =>
                {
                    b.HasOne("LibrarySystemModels.Person", "Person")
                        .WithMany("Users")
                        .HasForeignKey("PersonId")
                        .HasConstraintName("FK__Users__Person_id__3D5E1FD2");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("LibrarySystemModels.Author", b =>
                {
                    b.Navigation("AuthorBooks");
                });

            modelBuilder.Entity("LibrarySystemModels.Book", b =>
                {
                    b.Navigation("AuthorBooks");

                    b.Navigation("BookGenres");

                    b.Navigation("BookLanguages");

                    b.Navigation("BookPublishers");

                    b.Navigation("BookStorages");

                    b.Navigation("Borrows");
                });

            modelBuilder.Entity("LibrarySystemModels.Genre", b =>
                {
                    b.Navigation("BookGenres");
                });

            modelBuilder.Entity("LibrarySystemModels.Language", b =>
                {
                    b.Navigation("BookLanguages");
                });

            modelBuilder.Entity("LibrarySystemModels.Person", b =>
                {
                    b.Navigation("Borrows");

                    b.Navigation("PersonPositions");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("LibrarySystemModels.Position", b =>
                {
                    b.Navigation("PersonPositions");
                });

            modelBuilder.Entity("LibrarySystemModels.Publisher", b =>
                {
                    b.Navigation("BookPublishers");
                });

            modelBuilder.Entity("LibrarySystemModels.Role", b =>
                {
                    b.Navigation("RoleUsers");
                });

            modelBuilder.Entity("LibrarySystemModels.Storage", b =>
                {
                    b.Navigation("BookStorages");
                });

            modelBuilder.Entity("LibrarySystemModels.User", b =>
                {
                    b.Navigation("RoleUsers");

                    b.Navigation("TableLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
