using LibrarySystemData.Infrastructure;
using LibrarySystemModels;

namespace LibrarySystemData.Repositories
{
    public class TableLogRepository : RepositoryBase<TableLog>, ITableLogRepository
    {
        public TableLogRepository(LibraryContext context) 
            : base(context) { }

        public  TableLog GetData(string tableName, int tableId)
        {
            return _dbSet.Where(a => a.TableName.Equals(tableName) && a.TableId.Equals(tableId)).FirstOrDefault();
        }  

    }
    public interface ITableLogRepository : IRepositoryBase<TableLog>
    {
        TableLog GetData(string tableName, int tableId);
      
    }
}

