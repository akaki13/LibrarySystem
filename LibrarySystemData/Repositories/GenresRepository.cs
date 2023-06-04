using LibrarySystemData.Infrastructure;
using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Repositories
{
    public class GenresRepository : RepositoryBase<Genre> , IGenresRepository
    {
        public GenresRepository(LibraryContext context)
        : base(context) { }
    }
    public interface IGenresRepository : IRepositoryBase<Genre>
    {
        
    }
}
