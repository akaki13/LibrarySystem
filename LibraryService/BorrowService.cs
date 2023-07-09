using LibrarySystemData.Repositories;
using LibrarySystemModels;

namespace LibraryService
{
    public class BorrowService : IBorrowService
    {
        public readonly IBorrowRepository _borrowRepository;
        
        public BorrowService(IBorrowRepository borrowRepository)
        {
            _borrowRepository = borrowRepository;
        }

        public List<Borrow> GetAll()
        {
            return _borrowRepository.TakeAll().Result;
        }

        public void Add(Borrow borrow)
        {
            _borrowRepository.AddData(borrow);
        }

        public void Save()
        {
            _borrowRepository.SaveData();
        }

        public Borrow GetById(int id)
        {
            return _borrowRepository.FindById(id).Result;
        }

        public void Delete(Borrow borrow)
        {
            _borrowRepository.DeleteData(borrow);
        }

        public void Update(Borrow borrow)
        {
            _borrowRepository.UpdateData(borrow);
        }
    }

    public interface IBorrowService
    {
        List<Borrow> GetAll();
        void Add(Borrow borrow);
        void Update(Borrow borrow);
        void Save();
        Borrow GetById(int id);
        void Delete(Borrow borrow);
    }
}
