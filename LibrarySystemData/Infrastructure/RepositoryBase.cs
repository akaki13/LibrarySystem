using LibrarySystemModels;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystemData.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    { 
        public readonly DbSet<T> _dbSet;
        public readonly LibraryContext _context;

        protected RepositoryBase(LibraryContext context)
        {
            _context = context;
            _dbSet = context.Set<T>(); 
        }

        public virtual async Task AddData(T data)
        {
            await _dbSet.AddAsync(data);
                 
        }
        public virtual void  DeleteData(T data)
        {
            _context.Remove(data);

        }

        public virtual async Task<List<T>> TakeAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual  void UpdateData(T data)
        {
            _dbSet.Update(data);
        }

        public virtual void  SaveData()
        {
            _context.SaveChanges();

        }

        public virtual async Task<T> FindById(int? id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
