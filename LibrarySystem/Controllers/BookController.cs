using LibraryService;
using LibrarySystem.Data;
using LibrarySystem.Models.View;
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
        public BookController(IBookService bookService, ITableLogService tableLogService, IAuthorBookService authorBookService, IBookGenreService bookGenreService,
            IBookStorageService bookStorageService, IBookPublisherService bookPublisherService, IBookLanguageService bookLanguageService)
        {
            _bookService = bookService;
            _tableLogService = tableLogService;
            _authorBookService = authorBookService;
            _bookLanguageService = bookLanguageService;
            _bookPublisherService = bookPublisherService;
            _bookStorageService = bookStorageService;
            _bookGenreService = bookGenreService;
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
            var bookPublishers= _bookPublisherService.GetAll();

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

            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddBook(AddBookView bookView)
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
                foreach (int i in bookView.AuthorId)
                {
                    var authorBookLog = _tableLogService.AddWithId(DataUtil.AuthorBookTableName, userID);
                    _tableLogService.Save();
                    _authorBookService.AddMultipleData(i, book.Id, authorBookLog.Id);
                }
                foreach (int i in bookView.GenreId)
                {
                    var genreBookLog = _tableLogService.AddWithId(DataUtil.BookGenreTableName, userID);
                    _tableLogService.Save();
                    _bookGenreService.AddMultipleData(i, book.Id, genreBookLog.Id);
                }
                foreach (int i in bookView.LanguageId)
                {
                    var log = _tableLogService.AddWithId(DataUtil.BookLanguageTableName, userID);
                    _tableLogService.Save();
                    _bookLanguageService.AddMultipleData(i, book.Id , log.Id);
                }
                foreach (int i in bookView.PublisherId)
                {
                    var log = _tableLogService.AddWithId(DataUtil.BookPublisherTableName, userID);
                    _tableLogService.Save();
                    _bookPublisherService.AddMultipleData(i, book.Id, log.Id);
                }
                foreach (int i in bookView.StorageId)
                {
                    var log = _tableLogService.AddWithId(DataUtil.BookStorageTableName, userID);
                    _tableLogService.Save();
                    _bookStorageService.AddMultipleData(i, book.Id, log.Id);
                }
                return RedirectToAction("Index", "Book");
            }
            else
            {
                return View(bookView);
            }

        }
    }
}
