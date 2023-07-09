using LibrarySystemData.Repositories;
using LibrarySystemModels;

namespace LibraryService
{
    public class AuthorBookService : IAuthorBookService
    {
        public readonly IAuthorBookRepository _authorBookRepository;

        public AuthorBookService(IAuthorBookRepository authorBookRepository)
        {
            _authorBookRepository = authorBookRepository;
        }

        public List<AuthorBook> GetAll()
        {
            return _authorBookRepository.TakeAll().Result;
        }

        public void Add(AuthorBook authorBook)
        {
            _authorBookRepository.AddData(authorBook);
        }

        public void Save()
        {
            _authorBookRepository.SaveData();
        }

        public AuthorBook GetById(int id)
        {
            return _authorBookRepository.FindById(id).Result;
        }

        public void Delete(AuthorBook authorBook)
        {
            _authorBookRepository.DeleteData(authorBook);
        }

        public void Update(AuthorBook authorBook)
        {
            _authorBookRepository.UpdateData(authorBook);
        }
        public AuthorBook AddData(int ints, int bookId)
        {
            var bookAuthor = new AuthorBook
            {
                BookId = bookId,
                AutorId = ints,

            };
            _authorBookRepository.AddData(bookAuthor);
            _authorBookRepository.SaveData();
            return bookAuthor;
        }

        public List<AuthorBook> GetByBookId(int id)
        {
            return _authorBookRepository.GetByBookId(id);
        }

        public List<AuthorBook> GetByAuthorId(int id)
        {
            return _authorBookRepository.GetByAuthorId(id);
        }
    }
    

    public interface IAuthorBookService
    {
        List<AuthorBook> GetAll();
        void Add(AuthorBook authorBook);
        void Update(AuthorBook authorBook);
        void Save();
        AuthorBook GetById(int id);
        void Delete(AuthorBook authorBook);
        AuthorBook AddData(int ints, int bookId);
        List<AuthorBook> GetByBookId(int id);
        List<AuthorBook> GetByAuthorId(int id);
    }
}
