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
        public void AddMultipleData(int ints , int bookId,int logId)
        {
            var bookAuthor = new AuthorBook
            {
                BookId = bookId,
                AutorId = ints,
                LogsId = logId
            };
            _authorBookRepository.AddData(bookAuthor);
            _authorBookRepository.SaveData();
            
        }

        public List<AuthorBook> GetByBookId(int id)
        {
            return _authorBookRepository.GetByBookId(id);
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
        void AddMultipleData(int ints, int bookId, int logId);
        List<AuthorBook> GetByBookId(int id);
    }
}
