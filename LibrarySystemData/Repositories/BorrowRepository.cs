using LibrarySystemData.Infrastructure;
using LibrarySystemModels;


namespace LibrarySystemData.Repositories
{
    public class BorrowRepository : RepositoryBase<Borrow>, IBorrowRepository
    {
        public BorrowRepository(LibraryContext context)
           : base(context) { }
        

    }

    public interface IBorrowRepository : IRepositoryBase<Borrow>
    {
    }
}
