using LibrarySystemData.Infrastructure;
using LibrarySystemModels;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystemData.Repositories
{
    public class UserRepository : RepositoryBase<User> , IUserRepository
    {
         public UserRepository(LibraryContext context)
            : base(context) { }

        public async Task<User> FindByUserName(string userName)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Login.Equals(userName));
        }

        public async Task<User> FindByLoginInfo(string userName,string password)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Password.Equals(password) && u.Login.Equals(userName));
        }

        public async Task<User> FindByPersonId(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.PersonId.Equals(id));
        }

        public virtual async Task<List<User>> TakeAllWithPerson()
        {
            return await _dbSet.Include(r => r.Person ).Include(r => r.RoleUsers)
                .ToListAsync();
        }

    }

    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> FindByUserName(string userName);
        Task<User> FindByLoginInfo(string userName, string password);
        Task<User> FindByPersonId(int id);
        Task<List<User>> TakeAllWithPerson();
    }
}
