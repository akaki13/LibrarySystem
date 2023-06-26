using AutoMapper;
using LibraryService;
using LibrarySystem.Data;
using LibrarySystem.Models.View;
using LibrarySystem.Util;
using LibrarySystemData.Repositories;
using LibrarySystemModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace LibrarySystem.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ITableLogService _tableLogService;
        private readonly IAuthorBookService _authorBookService;
        private readonly IBookGenreService _bookGenreService;
        private readonly IBookLanguageService _bookLanguageService;
        private readonly IBookPublisherService _bookPublisherService;
        private readonly IBookStorageService _bookStorageService;
        private readonly IMapper _mapper;
        public BookController(IBookService bookService, ITableLogService tableLogService, IAuthorBookService authorBookService, IBookGenreService bookGenreService,
            IBookStorageService bookStorageService, IBookPublisherService bookPublisherService, IBookLanguageService bookLanguageService,
            IMapper mapper)
        {
            _bookService = bookService;
            _tableLogService = tableLogService;
            _authorBookService = authorBookService;
            _bookLanguageService = bookLanguageService;
            _bookPublisherService = bookPublisherService;
            _bookStorageService = bookStorageService;
            _bookGenreService = bookGenreService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<Book>> GetBook()
        {
            var books = _bookService.GetAll();

            return books;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<AuthorBook>> GetBookAuthor()
        {
            var authorBooks = _authorBookService.GetAll();

            return authorBooks;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<BookGenre>> GetBookGenre()
        {
            var bookGenres = _bookGenreService.GetAll();

            return bookGenres;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<BookLanguage>> GetBookLanguage()
        {
            var bookLanguages = _bookLanguageService.GetAll();

            return bookLanguages;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<BookPublisher>> GetBookPublisher()
        {
            var bookPublishers = _bookPublisherService.GetAll();

            return bookPublishers;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<BookStorage>> GetBookStorage()
        {
            var bookStorages = _bookStorageService.GetAll();
            return bookStorages;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddBook()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UpdateBook(int id)
        {
            var book = _bookService.GetById(id);
            UpdateBookView bookview = CreateData(id, book);
            return View(bookview);
        }

       

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult UpdateBook(UpdateBookView updateBookView)
        {
            if (ModelState.IsValid)
            {
                var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var book = _bookService.GetById(updateBookView.Id);
                _mapper.Map(book, updateBookView);
                _bookService.Update(book);
                _tableLogService.Update(book.LogsId);
                _tableLogService.Save();
                DeleteData(updateBookView);
                var bookview = _mapper.Map<BookView>(updateBookView);
                AddData(bookview, userID, book.Id);
                return RedirectToAction("Index", "Book");
            }
            else
            {
                return View(updateBookView);
            }
            
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult DeleteBook(int id)
        {
            var book = _bookService.GetById(id);
            if (book != null)
            {
                
                UpdateBookView bookview = CreateData(id, book);
                DeleteData(bookview);
                _bookService.Delete(book);
                _bookService.Save();
                _tableLogService.Delete(book.LogsId);
                _tableLogService.Save();
                return ResultApi.Succeeded();
            }
            else
            {
                return ResultApi.Failed();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddBook(BookView bookView)
        {
            if (ModelState.IsValid)
            {
                int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var bookLog = _tableLogService.AddWithId(DataUtil.BookTableName, userID);
                _tableLogService.Save();
                var book = new Book
                {
                    Name = bookView.Name,
                    Description = bookView.Description,
                    LogsId = bookLog.Id,
                };
                _bookService.Add(book);
                _bookService.Save();
                AddData(bookView, userID, book.Id);
                return RedirectToAction("Index", "Book");
            }
            else
            {
                return View(bookView);
            }
        }

        private void AddData(BookView bookView, int userID, int id)
        {
            foreach (int i in bookView.AuthorId)
            {
                var authorBookLog = _tableLogService.AddWithId(DataUtil.AuthorBookTableName, userID);
                _tableLogService.Save();
                _authorBookService.AddMultipleData(i, id, authorBookLog.Id);
            }
            foreach (int i in bookView.GenreId)
            {
                var genreBookLog = _tableLogService.AddWithId(DataUtil.BookGenreTableName, userID);
                _tableLogService.Save();
                _bookGenreService.AddMultipleData(i, id, genreBookLog.Id);
            }
            foreach (int i in bookView.LanguageId)
            {
                var log = _tableLogService.AddWithId(DataUtil.BookLanguageTableName, userID);
                _tableLogService.Save();
                _bookLanguageService.AddMultipleData(i, id, log.Id);
            }
            foreach (int i in bookView.PublisherId)
            {
                var log = _tableLogService.AddWithId(DataUtil.BookPublisherTableName, userID);
                _tableLogService.Save();
                _bookPublisherService.AddMultipleData(i, id, log.Id);
            }
            foreach (int i in bookView.StorageId)
            {
                var log = _tableLogService.AddWithId(DataUtil.BookStorageTableName, userID);
                _tableLogService.Save();
                _bookStorageService.AddMultipleData(i, id, log.Id);
            }
        }

        private void DeleteData(UpdateBookView bookView)
        {
            var genres = _bookGenreService.GetByBookId(bookView.Id);
            foreach (var genre in genres)
            {
                _tableLogService.Delete(genre.LogsId);
                _tableLogService.Save();
                _bookGenreService.Delete(genre);
                _bookGenreService.Save();
            }
            var languages = _bookLanguageService.GetByBookId(bookView.Id);
            foreach (var language in languages)
            {
                _tableLogService.Delete(language.LogsId);
                _tableLogService.Save();
                _bookLanguageService.Delete(language);
                _bookGenreService.Save();
            }
            var publishers = _bookPublisherService.GetByBookId(bookView.Id);
            foreach (var publisher in publishers)
            {
                _tableLogService.Delete(publisher.LogsId);
                _tableLogService.Save();
                _bookPublisherService.Delete(publisher);
                _bookPublisherService.Save();
            }
            var storages = _bookStorageService.GetByBookId(bookView.Id);
            foreach (var storage in storages)
            {
                _tableLogService.Delete(storage.LogsId);
                _tableLogService.Save();
                _bookStorageService.Delete(storage);
                _bookStorageService.Save();
            }
            var authors = _authorBookService.GetByBookId(bookView.Id);
            foreach (var author in authors)
            {
                _tableLogService.Delete(author.LogsId);
                _tableLogService.Save();
                _authorBookService.Delete(author);
                _authorBookService.Save();
            }
        }

        private UpdateBookView CreateData(int id, Book book)
        {
            var bookview = _mapper.Map<UpdateBookView>(book);
            var genres = _bookGenreService.GetByBookId(id);
            bookview.GenreId = new List<int>();
            foreach (var genre in genres)
            {
                bookview.GenreId.Add((int)genre.GenreId);
            }
            var languages = _bookLanguageService.GetByBookId(id);
            bookview.LanguageId = new List<int>();
            foreach (var language in languages)
            {
                bookview.LanguageId.Add((int)language.LanguagesId);
            }
            var authors = _authorBookService.GetByBookId(id);
            bookview.AuthorId = new List<int>();
            foreach (var author in authors)
            {
                bookview.AuthorId.Add((int)author.AutorId);
            }
            var publishers = _bookPublisherService.GetByBookId(id);
            bookview.PublisherId = new List<int>();
            foreach (var publisher in publishers)
            {
                bookview.PublisherId.Add((int)publisher.PublisherId);
            }
            var storages = _bookStorageService.GetByBookId(id);
            bookview.StorageId = new List<int>();
            foreach (var storage in storages)
            {
                bookview.StorageId.Add((int)storage.StorageId);
            }

            return bookview;
        }
    }
}
