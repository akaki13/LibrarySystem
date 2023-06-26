using LibrarySystemData.Infrastructure;
using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Repositories
{
    public class BookLanguageRepository : RepositoryBase<BookLanguage>, IBookLanguageRepository
    {
        public BookLanguageRepository(LibraryContext context)
           : base(context) { }
        public List<BookLanguage> GetByBookId(int id)
        {
            return _dbSet.Where(i => i.BookId.Equals(id)).ToList();
        }

    }

    public interface IBookLanguageRepository : IRepositoryBase<BookLanguage>
    {
        List<BookLanguage> GetByBookId(int id);
    }
}
