using LibrarySystemData.Infrastructure;
using LibrarySystemModels;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystemData.Repositories
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(LibraryContext context)
           : base(context) { }

        public async Task<Role> TakeByTitle(string title) 
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Title == title);
        }
    }

    public interface IRoleRepository : IRepositoryBase<Role>
    {
        Task<Role> TakeByTitle(string title);
    }
}
