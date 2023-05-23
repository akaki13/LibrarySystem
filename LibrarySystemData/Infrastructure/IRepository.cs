using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LibrarySystemModels;

namespace LibrarySystemData.Infrastructure
{
    public interface IRepositoryBase<T> where T : class
    {
        Task AddData(T data);
        void UpdateData(T entity);
        void SaveData();
        Task<T> FindById (int? id);
        Task<List<T>> TakeAll();
    }
}
