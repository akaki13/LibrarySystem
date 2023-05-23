using LibrarySystemData.Repositories;
using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Update(int id)
        {
             _tableLogRepository.UpdateData(id);
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

    }
}
