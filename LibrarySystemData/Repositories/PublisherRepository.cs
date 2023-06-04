using LibrarySystemData.Infrastructure;
using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Repositories
{
    public class PublisherRepository : RepositoryBase<Publisher> , IPublisherRepository
    {
        public PublisherRepository(LibraryContext context)
            : base(context) { }

    }
    public interface IPublisherRepository : IRepositoryBase<Publisher>
    {
    }
}
