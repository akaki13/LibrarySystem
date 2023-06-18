﻿using LibrarySystemData.Repositories;
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
        public void AddMultipleData(int ints, int bookId, int logId)
        {
            var data = new BookGenre
            {
                BookId = bookId,
                GenreId = ints,
                LogsId = logId,
            };
            _bookGenreRepository.AddData(data);
            _bookGenreRepository.SaveData();
            
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
        void AddMultipleData(int ints, int bookId, int logId);

    }
}
