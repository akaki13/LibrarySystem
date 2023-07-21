using LibrarySystemData.Configuration;
using LibrarySystemModels;
using LibrarySystemModels.Procedure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonPosition> PersonPositions { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleUser> RoleUsers { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
        public virtual DbSet<TableLog> TableLogs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public DbSet<ByPopularity> ByPopularity { get; set; }
        public DbSet<ClientsPerformance> ClientsPerformance { get; set; }
        public DbSet<OverdueTransactions> OverdueTransactions { get; set; }
        public DbSet<CurrentTransactions> CurrentTransactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-47MVG9S;Database=library;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorBookConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new BookGenreConfiguration());
            modelBuilder.ApplyConfiguration(new BookLanguageConfiguration());
            modelBuilder.ApplyConfiguration(new BookPublisherConfiguration());
            modelBuilder.ApplyConfiguration(new BookStorageConfiguration());
            modelBuilder.ApplyConfiguration(new BorrowConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new PersonPositionConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleUserConfiguration());
            modelBuilder.ApplyConfiguration(new SliderConfiguration());
            modelBuilder.ApplyConfiguration(new StorageConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TableLogConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
