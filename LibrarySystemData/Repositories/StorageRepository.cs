using LibrarySystemData.Infrastructure;
using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Repositories
{
    public class StorageRepository : RepositoryBase<Storage>, IStorageRepository
    {
        public StorageRepository(LibraryContext context)
           : base(context) { }

    }

    public interface IStorageRepository : IRepositoryBase<Storage>
    {

    }
}
