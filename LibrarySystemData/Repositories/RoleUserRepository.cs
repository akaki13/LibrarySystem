using LibrarySystemData.Infrastructure;
using LibrarySystemModels;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystemData.Repositories
{
    public class RoleUserRepository : RepositoryBase<RoleUser> , IRoleUserRepository
    {
        public RoleUserRepository(LibraryContext context)
           : base(context) { }

        public async Task<RoleUser> FindByUserId(int id)
        {
            
            return  await _dbSet.Include(r => r.Role)
                   .FirstOrDefaultAsync(u => u.UsersId.Equals(id));
        }
    }

    public interface IRoleUserRepository : IRepositoryBase<RoleUser>
    {
        Task<RoleUser> FindByUserId(int id);
    }
}
