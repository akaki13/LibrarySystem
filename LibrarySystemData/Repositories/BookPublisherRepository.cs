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

    }

    public interface IBookPublisherRepository : IRepositoryBase<BookPublisher>
    {

    }
}
