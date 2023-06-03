using LibrarySystemData.Repositories;
using LibrarySystemModels;

namespace LibraryService
{
    public class TableLogService : ITableLogService
    {
        private readonly ITableLogRepository _tableLogRepository;

        public TableLogService(ITableLogRepository tableLogRepository)
        {
            _tableLogRepository = tableLogRepository;
        }

        public TableLog Add(string tableName)
        {
            return _tableLogRepository.AddData(tableName).Result;
        }
        public TableLog AddWithId(string tableName,int id)
        {
            return _tableLogRepository.AddDataWithId(tableName,id).Result;
        }

        public void Update(int id)
        {
             _tableLogRepository.UpdateData(id);
        }

        public void Delete(int id)
        {
            _tableLogRepository.DeleteData(id);
        }

        public void Save()
        {
            _tableLogRepository.SaveData();
        }

    }

    public interface ITableLogService
    {
        TableLog Add(string tableName);
        void Update(int id);
        void Save();
        void Delete(int id);
        TableLog AddWithId(string tableName, int id);

    }
}
