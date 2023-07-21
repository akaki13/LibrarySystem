using LibrarySystemData.Infrastructure;
using LibrarySystemModels;
using LibrarySystemModels.Procedure;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystemData.Repositories
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository (LibraryContext context)
            :base(context) { }

        public async Task<Person> FindByEmail(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<Person> FindByPhone(string phone)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Phone.Equals(phone));
        }

    }

    public interface IPersonRepository : IRepositoryBase<Person>
    {
        Task<Person> FindByEmail(string email);
        Task<Person> FindByPhone(string phone);

    }
}
