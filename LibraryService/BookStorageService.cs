﻿using LibrarySystemData.Repositories;
using LibrarySystemModels;

namespace LibraryService
{
    public class BookStorageService : IBookStorageService
    {
        public readonly IBookStorageRepository _bookStorageRepository;
        
        public BookStorageService(IBookStorageRepository bookStorageRepository)
        {
            _bookStorageRepository = bookStorageRepository;
        }

        public List<BookStorage> GetAll()
        {
            return _bookStorageRepository.TakeAll().Result;
        }

        public void Add(BookStorage bookStorage)
        {
            _bookStorageRepository.AddData(bookStorage);
        }

        public void Save()
        {
            _bookStorageRepository.SaveData();
        }

        public BookStorage GetById(int id)
        {
            return _bookStorageRepository.FindById(id).Result;
        }

        public void Delete(BookStorage bookStorage)
        {
            _bookStorageRepository.DeleteData(bookStorage);
        }

        public void Update(BookStorage bookStorage)
        {
            _bookStorageRepository.UpdateData(bookStorage);
        }
        public BookStorage AddData(int ints, int bookId)
        {
            var data = new BookStorage
            {
                BookId = bookId,
                StorageId = ints,
            };
            _bookStorageRepository.AddData(data);
            _bookStorageRepository.SaveData();
            return data;
        }

        public List<BookStorage> GetByBookId(int id)
        {
            return _bookStorageRepository.GetByBookId(id);
        }

        public List<BookStorage> GetByStorageId(int id)
        {
            return _bookStorageRepository.GetByStorageId(id);
        }
    }

    public interface IBookStorageService
    {
        List<BookStorage> GetAll();
        void Add(BookStorage bookStorage);
        void Update(BookStorage bookStorage);
        void Save();
        BookStorage GetById(int id);
        void Delete(BookStorage bookStorage);
        BookStorage AddData(int ints, int bookId);
        List<BookStorage> GetByBookId(int id);
        List<BookStorage> GetByStorageId(int id);
    }
}
