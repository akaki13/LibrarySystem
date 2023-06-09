﻿using LibrarySystemData.Repositories;
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
        public BookPublisher AddData(int ints, int bookId)
        {
            var data = new BookPublisher
            {
                BookId = bookId,
                PublisherId = ints,
            };
            _bookPublisherRepository.AddData(data);
            _bookPublisherRepository.SaveData();
            return data;
            
        }

        public List<BookPublisher> GetByBookId(int id)
        {
            return _bookPublisherRepository.GetByBookId(id);
        }
        public List<BookPublisher> GetByPublisherId(int id)
        {
            return _bookPublisherRepository.GetByPublisherId(id);
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
        BookPublisher AddData(int ints, int bookId);
        List<BookPublisher> GetByBookId(int id);
        List<BookPublisher> GetByPublisherId(int id);
    }
}
