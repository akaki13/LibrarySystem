using LibrarySystemData.Repositories;
using LibrarySystemModels;
using System.Security.Cryptography;

namespace LibraryService
{
    public class TableLogService : ITableLogService
    {
        private readonly ITableLogRepository _tableLogRepository;

        public TableLogService(ITableLogRepository tableLogRepository)
        {
            _tableLogRepository = tableLogRepository;
        }

        public void AddData(string tableName, int tableId, string status, string description, int? userID)
        {
            var tableLog = new TableLog
            {
                TableName = tableName,
                TableId = tableId,
                CreateDate = DateTime.Now,
                Status = status,
                Description = description,
                UserId = userID,
            };
            _tableLogRepository.AddData(tableLog);
            _tableLogRepository.SaveData();
        }

        public void Update(string tableName, int tableId, string status, string description)
        {
            var tableLog = _tableLogRepository.GetData(tableName, tableId);
            tableLog.ChangeDate = DateTime.Now;
            tableLog.Status = status;
            tableLog.Description = description;
            _tableLogRepository.UpdateData(tableLog);
            _tableLogRepository.SaveData();
        }

        public void Delete(string tableName, int tableId, string status, string description)
        {
            var tableLog = _tableLogRepository.GetData(tableName, tableId);
            tableLog.DeleteDate = DateTime.Now;
            tableLog.Status = status;
            tableLog.Description = description;
            _tableLogRepository.UpdateData(tableLog);
            _tableLogRepository.SaveData();
        }
        public void Discard()
        {
            _tableLogRepository.DiscardChanges();
        }
    }

    public interface ITableLogService
    {
        void AddData(string tableName, int tableId, string status, string description, int? userID);
        void Update(string tableName, int tableId, string status, string description);
        void Delete(string tableName, int tableId, string status, string description);
        void Discard();
    }
}
