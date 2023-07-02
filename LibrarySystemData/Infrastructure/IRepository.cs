

namespace LibrarySystemData.Infrastructure
{
    public interface IRepositoryBase<T> where T : class
    {
        Task AddData(T data);
        void UpdateData(T entity);
        void SaveData();
        Task<T> FindById (int? id);
        Task<List<T>> TakeAll();
        void DeleteData(T data);
        void DiscardChanges();
    }
}
