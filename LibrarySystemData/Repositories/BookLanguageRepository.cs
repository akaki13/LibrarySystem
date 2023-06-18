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

    }

    public interface IBookLanguageRepository : IRepositoryBase<BookLanguage>
    {

    }
}
