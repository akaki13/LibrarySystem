using LibrarySystemData.Repositories;
using LibrarySystemModels;

namespace LibraryService
{
    public class BookPublisherService : IBookPublisherService
    {
        public readonly IBookPublisherRepository _bookPublisherRepository;
        
        public BookPublisherService(IBookPublisherRepository bookPublisherRepository)
        {
            _bookPublisherRepository = bookPublisherRepository;
        }

        public List<BookPublisher> GetAll()
        {
            return _bookPublisherRepository.TakeAll().Result;
        }

        public void Add(BookPublisher bookPublisher)
        {
            _bookPublisherRepository.AddData(bookPublisher);
        }

        public void Save()
        {
            _bookPublisherRepository.SaveData();
        }

        public BookPublisher GetById(int id)
        {
            return _bookPublisherRepository.FindById(id).Result;
        }

        public void Delete(BookPublisher bookPublisher)
        {
            _bookPublisherRepository.DeleteData(bookPublisher);
        }

        public void Update(BookPublisher bookPublisher)
        {
            _bookPublisherRepository.UpdateData(bookPublisher);
        }
        public void AddMultipleData(int ints, int bookId, int logId)
        {
            var data = new BookPublisher
            {
                BookId = bookId,
                PublisherId = ints,
                LogsId = logId,
            };
            _bookPublisherRepository.AddData(data);
            _bookPublisherRepository.SaveData();
            
        }

        public List<BookPublisher> GetByBookId(int id)
        {
            return _bookPublisherRepository.GetByBookId(id);
        }
    }

    public interface IBookPublisherService
    {
        List<BookPublisher> GetAll();
        void Add(BookPublisher bookPublisher);
        void Update(BookPublisher bookPublisher);
        void Save();
        BookPublisher GetById(int id);
        void Delete(BookPublisher bookPublisher);
        void AddMultipleData(int ints, int bookId, int logId);
        List<BookPublisher> GetByBookId(int id);
    }
}
