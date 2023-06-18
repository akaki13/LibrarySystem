using LibrarySystemData.Repositories;
using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService
{
    public class BookService : IBookService
    {
        public readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public List<Book> GetAll()
        {
            return _bookRepository.TakeAll().Result;
        }

        public List<Book> GetAllWithTables()
        {
            return _bookRepository.TakeWitTables().Result;
        }

        public void Add(Book book)
        {
            _bookRepository.AddData(book);
        }

        public void Save()
        {
            _bookRepository.SaveData();
        }

        public Book GetById(int id)
        {
            return _bookRepository.FindById(id).Result;
        }

        public void Delete(Book book)
        {
            _bookRepository.DeleteData(book);
        }

        public void Update(Book book)
        {
            _bookRepository.UpdateData(book);
        }
    }
    public interface IBookService
    {
        List<Book> GetAll();
        void Add(Book book);
        void Update(Book book);
        void Save();
        Book GetById(int id);
        void Delete(Book book);
        List<Book> GetAllWithTables();
    }
}
