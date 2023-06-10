using LibrarySystemData.Repositories;
using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService
{
    public class StorageService : IStorageService
    {
        public readonly IStorageRepository _storageRepository;

        public StorageService(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }
        public List<Storage> GetAll()
        {
            return _storageRepository.TakeAll().Result;
        }

        public void Add(Storage storage)
        {
            _storageRepository.AddData(storage);
        }

        public void Save()
        {
            _storageRepository.SaveData();
        }

        public Storage GetById(int id)
        {
            return _storageRepository.FindById(id).Result;
        }

        public void Delete(Storage storage)
        {
            _storageRepository.DeleteData(storage);
        }

        public void Update(Storage storage)
        {
            _storageRepository.UpdateData(storage);
        }
    }
    public interface IStorageService
    {
        List<Storage> GetAll();
        void Add(Storage storage);
        void Update(Storage storage);
        void Save();
        Storage GetById(int id);
        void Delete(Storage storage);
    }
}
