using LibrarySystemData.Infrastructure;
using LibrarySystemModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Repositories
{
    public class BookRepository : RepositoryBase<Book> , IBookRepository
    {
        public BookRepository(LibraryContext context)
        : base(context) { }

        public virtual async Task<List<Book>> TakeWitTables()
        {
            return await _dbSet.Include(r => r.AuthorBooks).ThenInclude(a => a.Autor)
                .Include(r => r.BookPublishers).ThenInclude(a => a.Publisher)
                .Include(r => r.BookGenres).ThenInclude(a => a.Genre)
                .Include(r => r.BookStorages).ThenInclude(a => a.Storage).ToListAsync();
        }
    }
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<List<Book>> TakeWitTables();
    }
}
