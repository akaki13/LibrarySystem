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
        public async Task UpdateData(int id)
        {
            var tableLog = await _dbSet.FindAsync(id);
            if (tableLog != null)
            {
                tableLog.ChangeDate = DateTime.Now;
            }
            _dbSet.Update(tableLog);
        }

    }
    public interface ITableLogRepository : IRepositoryBase<TableLog>
    {
        Task UpdateData(int id);
        Task<TableLog> AddData(string tableName);

    }
}

