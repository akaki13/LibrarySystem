using LibrarySystemData.Repositories;
using LibrarySystemModels;

namespace LibraryService
{
    public class BookLanguageService : IBookLanguageService
    {
        public readonly IBookLanguageRepository _bookLanguageRepository;
        
        public BookLanguageService(IBookLanguageRepository bookLanguageRepository)
        {
            _bookLanguageRepository = bookLanguageRepository;
        }

        public List<BookLanguage> GetAll()
        {
            return _bookLanguageRepository.TakeAll().Result;
        }

        public void Add(BookLanguage bookLanguage)
        {
            _bookLanguageRepository.AddData(bookLanguage);
        }

        public void Save()
        {
            _bookLanguageRepository.SaveData();
        }

        public BookLanguage GetById(int id)
        {
            return _bookLanguageRepository.FindById(id).Result;
        }

        public void Delete(BookLanguage bookLanguage)
        {
            _bookLanguageRepository.DeleteData(bookLanguage);
        }

        public void Update(BookLanguage bookLanguage)
        {
            _bookLanguageRepository.UpdateData(bookLanguage);
        }
        public void AddMultipleData(int ints, int bookId, int logId)
        {
            var data = new BookLanguage
            {
                BookId = bookId,
                LanguagesId = ints,
                LogsId = logId,
            };
            _bookLanguageRepository.AddData(data);
            _bookLanguageRepository.SaveData();
            
        }
    }

    public interface IBookLanguageService
    {
        List<BookLanguage> GetAll();
        void Add(BookLanguage bookLanguage);
        void Update(BookLanguage bookLanguage);
        void Save();
        BookLanguage GetById(int id);
        void AddMultipleData(int ints, int bookId, int logId);
        void Delete(BookLanguage bookLanguage);
    }
}
