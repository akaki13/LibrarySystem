using LibrarySystemData.Infrastructure;
using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Repositories
{
    public class BookPublisherRepository : RepositoryBase<BookPublisher>, IBookPublisherRepository
    {
        public BookPublisherRepository(LibraryContext context)
           : base(context) { }
        public List<BookPublisher> GetByBookId(int id)
        {
            return _dbSet.Where(i => i.BookId.Equals(id)).ToList();
        }

        public List<BookPublisher> GetByPublisherId(int id)
        {
            return _dbSet.Where(i => i.PublisherId.Equals(id)).ToList();
        }
    }

    public interface IBookPublisherRepository : IRepositoryBase<BookPublisher>
    {
        List<BookPublisher> GetByBookId(int id);
        List<BookPublisher> GetByPublisherId(int id);
    }
}
