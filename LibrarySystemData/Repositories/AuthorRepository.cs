using LibrarySystemData.Infrastructure;
using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Repositories
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryContext context)
           : base(context) { }

    }

    public interface IAuthorRepository : IRepositoryBase<Author>
    {

    }
}
