using LibrarySystemData.Infrastructure;
using LibrarySystemModels;

namespace LibrarySystemData.Repositories
{
    public class TableLogRepository : RepositoryBase<TableLog>, ITableLogRepository
    {
        public TableLogRepository(LibraryContext context) 
            : base(context) { }

        public  async Task<TableLog> AddData(string tableName)
        {
            var tableLog = new TableLog
            {
                TableName = tableName,
                CreateDate = DateTime.Now,
            };
            await _dbSet.AddAsync(tableLog);
            return tableLog;
        }
        public async Task<TableLog> AddDataWithId(string tableName, int id)
        {
            var tableLog = new TableLog
            {
                TableName = tableName,
                CreateDate = DateTime.Now,
                UserId = id,
            };
            await _dbSet.AddAsync(tableLog);
            return tableLog;
        }
        public void UpdateData(int id)
        {
            var tableLog = FindById(id).Result;
            
            tableLog.ChangeDate = DateTime.Now;
            
            UpdateData(tableLog);
        }
        public void DeleteData(int id)
        {
            var tableLog = FindById(id).Result;

            tableLog.DeleteDate = DateTime.Now;

            UpdateData(tableLog);
        }

    }
    public interface ITableLogRepository : IRepositoryBase<TableLog>
    {
        void UpdateData(int id);
        Task<TableLog> AddData(string tableName);
        void DeleteData(int id);
        Task<TableLog> AddDataWithId(string tableName, int id);

    }
}

