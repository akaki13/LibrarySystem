using LibrarySystemData.Infrastructure;
using LibrarySystemModels;


namespace LibrarySystemData.Repositories
{
    public class AuthorBookRepository : RepositoryBase<AuthorBook>, IAuthorBookRepository
    {
        public AuthorBookRepository(LibraryContext context)
           : base(context) { }
        public List<AuthorBook> GetByBookId(int id)
        {
            return _dbSet.Where(i => i.BookId.Equals(id)).ToList();
        }

        public List<AuthorBook> GetByAuthorId(int id)
        {
            return _dbSet.Where(i => i.AutorId.Equals(id)).ToList();
        }

    }

    public interface IAuthorBookRepository : IRepositoryBase<AuthorBook>
    {
        List<AuthorBook> GetByBookId(int id);
        List<AuthorBook> GetByAuthorId(int id);
    }
}
