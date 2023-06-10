using LibrarySystemData.Infrastructure;
using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Repositories
{
    public class LanguageRepository : RepositoryBase<Language>, ILanguageRepository
    {
        public LanguageRepository(LibraryContext context)
           : base(context) { }

    }

    public interface ILanguageRepository : IRepositoryBase<Language> 
    {

    }
}
