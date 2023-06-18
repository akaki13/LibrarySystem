using System;
using System.Collections.Generic;
using LibrarySystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LibrarySystemData
{
    public partial class LibraryContext : DbContext
    {
        public LibraryContext()
        {
        }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<AuthorBook> AuthorBooks { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookGenre> BookGenres { get; set; }
        public virtual DbSet<BookLanguage> BookLanguages { get; set; }
        public virtual DbSet<BookPublisher> BookPublishers { get; set; }
        public virtual DbSet<BookStorage> BookStorages { get; set; }
        public virtual DbSet<Borrow> Borrows { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonPosition> PersonPositions { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleUser> RoleUsers { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
        public virtual DbSet<TableLog> TableLogs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
/*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-47MVG9S;Database=library1;Trusted_Connection=True;MultipleActiveResultSets=true");*/
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.Authors)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Author__Logs_id__403A8C7D");
            });

            modelBuilder.Entity<AuthorBook>(entity =>
            {
                entity.ToTable("Author_Book");

                entity.Property(e => e.AutorId).HasColumnName("Autor_id");

                entity.Property(e => e.BookId).HasColumnName("Book_id");

                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.HasOne(d => d.Autor)
                    .WithMany(p => p.AuthorBooks)
                    .HasForeignKey(d => d.AutorId)
                    .HasConstraintName("FK__Author_Bo__Autor__5EBF139D");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.AuthorBooks)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Author_Bo__Book___5DCAEF64");

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.AuthorBooks)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Author_Bo__Logs___5CD6CB2B");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Book__Logs_id__4BAC3F29");
            });

            modelBuilder.Entity<BookGenre>(entity =>
            {
                entity.ToTable("Book_Genre");

                entity.Property(e => e.BookId).HasColumnName("Book_id");

                entity.Property(e => e.GenreId).HasColumnName("Genre_id");

                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookGenres)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Book_Genr__Book___59063A47");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.BookGenres)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK__Book_Genr__Genre__59FA5E80");

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.BookGenres)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Book_Genr__Logs___5812160E");
            });

            modelBuilder.Entity<BookLanguage>(entity =>
            {
                entity.ToTable("Book_Languages");

                entity.Property(e => e.BookId).HasColumnName("Book_id");

                entity.Property(e => e.LanguagesId).HasColumnName("Languages_id");

                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookLanguages)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Book_Lang__Book___19DFD96B");

                entity.HasOne(d => d.Languages)
                    .WithMany(p => p.BookLanguages)
                    .HasForeignKey(d => d.LanguagesId)
                    .HasConstraintName("FK__Book_Lang__Langu__1AD3FDA4");

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.BookLanguages)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Book_Lang__Logs___18EBB532");
            });

            modelBuilder.Entity<BookPublisher>(entity =>
            {
                entity.ToTable("Book_Publisher");

                entity.Property(e => e.BookId).HasColumnName("Book_id");

                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.Property(e => e.PublisherId).HasColumnName("Publisher_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookPublishers)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Book_Publ__Book___5441852A");

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.BookPublishers)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Book_Publ__Logs___534D60F1");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.BookPublishers)
                    .HasForeignKey(d => d.PublisherId)
                    .HasConstraintName("FK__Book_Publ__Publi__5535A963");
            });

            modelBuilder.Entity<BookStorage>(entity =>
            {
                entity.ToTable("Book_Storage");

                entity.Property(e => e.BookId).HasColumnName("Book_id");

                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.Property(e => e.StorageId).HasColumnName("Storage_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookStorages)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Book_Stor__Book___4F7CD00D");

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.BookStorages)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Book_Stor__Logs___4E88ABD4");

                entity.HasOne(d => d.Storage)
                    .WithMany(p => p.BookStorages)
                    .HasForeignKey(d => d.StorageId)
                    .HasConstraintName("FK__Book_Stor__Stora__5070F446");
            });

            modelBuilder.Entity<Borrow>(entity =>
            {
                entity.Property(e => e.ActualReturnedTime)
                    .HasColumnType("date")
                    .HasColumnName("Actual_returned_time");

                entity.Property(e => e.BookId).HasColumnName("Book_id");

                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.Property(e => e.PersonId).HasColumnName("Person_id");

                entity.Property(e => e.ReturnedTime)
                    .HasColumnType("date")
                    .HasColumnName("Returned_time");

                entity.Property(e => e.TakeTime)
                    .HasColumnType("date")
                    .HasColumnName("Take_time");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Borrows)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Borrows__Book_id__72C60C4A");

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.Borrows)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Borrows__Logs_id__70DDC3D8");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Borrows)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__Borrows__Person___71D1E811");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.Genres)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Genre__Logs_id__45F365D3");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.Languages)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Languages__Logs___160F4887");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("Date_ofBirth");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailIsConfiormed).HasColumnName("Email_isConfiormed");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Person__Logs_id__398D8EEE");
            });

            modelBuilder.Entity<PersonPosition>(entity =>
            {
                entity.ToTable("Person_Position");

                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.Property(e => e.PersonId).HasColumnName("Person_id");

                entity.Property(e => e.PositionId).HasColumnName("Position_id");

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.PersonPositions)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Person_Po__Logs___6C190EBB");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonPositions)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__Person_Po__Perso__6E01572D");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.PersonPositions)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK__Person_Po__Posit__6D0D32F4");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("Position");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.Positions)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Position__Logs_i__6477ECF3");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("Publisher");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.Publishers)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Publisher__Logs___48CFD27E");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Role__Logs_id__619B8048");
            });

            modelBuilder.Entity<RoleUser>(entity =>
            {
                entity.ToTable("Role_Users");

                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.Property(e => e.RoleId).HasColumnName("Role_id");

                entity.Property(e => e.UsersId).HasColumnName("Users_id");

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.RoleUsers)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Role_User__Logs___6754599E");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleUsers)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Role_User__Role___68487DD7");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.RoleUsers)
                    .HasForeignKey(d => d.UsersId)
                    .HasConstraintName("FK__Role_User__Users__693CA210");
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storage");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.Storages)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Storage__Logs_id__4316F928");
            });

            modelBuilder.Entity<TableLog>(entity =>
            {
                entity.Property(e => e.ChangeDate)
                    .HasColumnType("date")
                    .HasColumnName("Change_date");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("date")
                    .HasColumnName("Create_date");

                entity.Property(e => e.DeleteDate)
                    .HasColumnType("date")
                    .HasColumnName("Delete_date");

                entity.Property(e => e.TableName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Table_name");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TableLogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_LogsUsers");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LogsId).HasColumnName("Logs_id");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PersonId).HasColumnName("Person_id");

                entity.HasOne(d => d.Logs)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.LogsId)
                    .HasConstraintName("FK__Users__Logs_id__3C69FB99");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__Users__Person_id__3D5E1FD2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
