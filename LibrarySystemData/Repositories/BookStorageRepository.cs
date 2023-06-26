using LibrarySystemData.Infrastructure;
using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Repositories
{
    public class BookStorageRepository : RepositoryBase<BookStorage>, IBookStorageRepository
    {
        public BookStorageRepository(LibraryContext context)
           : base(context) { }
        public List<BookStorage> GetByBookId(int id)
        {
            return _dbSet.Where(i => i.BookId.Equals(id)).ToList();
        }
    }

    public interface IBookStorageRepository : IRepositoryBase<BookStorage>
    {
        List<BookStorage> GetByBookId(int id);

    }
}
