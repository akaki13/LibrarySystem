using LibrarySystemData.Infrastructure;
using LibrarySystemModels;


namespace LibrarySystemData.Repositories
{
    public class AuthorBookRepository : RepositoryBase<AuthorBook>, IAuthorBookRepository
    {
        public AuthorBookRepository(LibraryContext context)
           : base(context) { }

    }

    public interface IAuthorBookRepository : IRepositoryBase<AuthorBook>
    {

    }
}
