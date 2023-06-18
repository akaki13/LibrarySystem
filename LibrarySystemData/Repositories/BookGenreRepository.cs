using LibrarySystemData.Infrastructure;
using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Repositories
{
    public class BookGenreRepository : RepositoryBase<BookGenre>, IBookGenreRepository
    {
        public BookGenreRepository(LibraryContext context)
           : base(context) { }

    }

    public interface IBookGenreRepository : IRepositoryBase<BookGenre>
    {

    }
}
