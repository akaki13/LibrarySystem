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
        public  BookLanguage AddData(int ints, int bookId)
        {
            var data = new BookLanguage
            {
                BookId = bookId,
                LanguagesId = ints,
            };
            _bookLanguageRepository.AddData(data);
            _bookLanguageRepository.SaveData();
            return data;
        }

        public List<BookLanguage> GetByBookId(int id)
        {
            return _bookLanguageRepository.GetByBookId(id);
        }
    }

    public interface IBookLanguageService
    {
        List<BookLanguage> GetAll();
        void Add(BookLanguage bookLanguage);
        void Update(BookLanguage bookLanguage);
        void Save();
        BookLanguage GetById(int id);
        BookLanguage AddData(int ints, int bookId);
        void Delete(BookLanguage bookLanguage);
        List<BookLanguage> GetByBookId(int id);
    }
}
