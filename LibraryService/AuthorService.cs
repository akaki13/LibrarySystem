using LibrarySystemData.Repositories;
using LibrarySystemModels;

namespace LibraryService
{
    public class AuthorService : IAuthorService
    {
        public readonly IAuthorRepository _authorRepository;
        
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public List<Author> GetAll()
        {
            return _authorRepository.TakeAll().Result;
        }

        public void Add(Author author)
        {
            _authorRepository.AddData(author);
        }

        public void Save()
        {
            _authorRepository.SaveData();
        }

        public Author GetById(int id)
        {
            return _authorRepository.FindById(id).Result;
        }

        public void Delete(Author author)
        {
            _authorRepository.DeleteData(author);
        }

        public void Update(Author author)
        {
            _authorRepository.UpdateData(author);
        }
    }

    public interface IAuthorService
    {
        List<Author> GetAll();
        void Add(Author author);
        void Update(Author author);
        void Save();
        Author GetById(int id);
        void Delete(Author author);
    }
}
