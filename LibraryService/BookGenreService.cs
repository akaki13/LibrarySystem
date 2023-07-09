using LibrarySystemData.Repositories;
using LibrarySystemModels;

namespace LibraryService
{
    public class BookGenreService : IBookGenreService
    {
        public readonly IBookGenreRepository _bookGenreRepository;
        
        public BookGenreService(IBookGenreRepository bookGenreRepository)
        {
            _bookGenreRepository = bookGenreRepository;
        }

        public List<BookGenre> GetAll()
        {
            return _bookGenreRepository.TakeAll().Result;
        }

        public void Add(BookGenre bookGenre)
        {
            _bookGenreRepository.AddData(bookGenre);
        }

        public void Save()
        {
            _bookGenreRepository.SaveData();
        }

        public BookGenre GetById(int id)
        {
            return _bookGenreRepository.FindById(id).Result;
        }

        public void Delete(BookGenre bookGenre)
        {
            _bookGenreRepository.DeleteData(bookGenre);
        }

        public void Update(BookGenre bookGenre)
        {
            _bookGenreRepository.UpdateData(bookGenre);
        }

        public BookGenre AddData(int ints, int bookId)
        {
            var data = new BookGenre
            {
                BookId = bookId,
                GenreId = ints,
            };
            _bookGenreRepository.AddData(data);
            _bookGenreRepository.SaveData();
            return data;
        }

        public List<BookGenre> GetByBookId(int id)
        {
            return _bookGenreRepository.GetByBookId(id);
        }

        public List<BookGenre> GetByGenreId(int id)
        {
            return _bookGenreRepository.GetByGenreId(id);
        }
    }

    public interface IBookGenreService
    {
        List<BookGenre> GetAll();
        void Add(BookGenre bookGenre);
        void Update(BookGenre bookGenre);
        void Save();
        BookGenre GetById(int id);
        void Delete(BookGenre bookGenre);
        BookGenre AddData(int ints, int bookId);
        List<BookGenre> GetByBookId(int id);
        List<BookGenre> GetByGenreId(int id);

    }
}
