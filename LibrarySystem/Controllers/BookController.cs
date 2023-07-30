using AutoMapper;
using LibraryService;
using LibrarySystem.Data;
using LibrarySystem.Models.View;
using LibrarySystem.Util;
using LibrarySystemModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IBookService bookService, ITableLogService tableLogService, IAuthorBookService authorBookService, IBookGenreService bookGenreService,
            IBookStorageService bookStorageService, IBookPublisherService bookPublisherService, IBookLanguageService bookLanguageService,
            IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _bookService = bookService;
            _tableLogService = tableLogService;
            _authorBookService = authorBookService;
            _bookLanguageService = bookLanguageService;
            _bookPublisherService = bookPublisherService;
            _bookStorageService = bookStorageService;
            _bookGenreService = bookGenreService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
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
                try
                {
                    _mapper.Map(updateBookView, book);
                    _bookService.Update(book);
                    _bookService.Save();
                    _tableLogService.Update(DataUtil.BookTableName, book.Id, DataUtil.TableStatusInfo, DataUtil.UpdateData);
                    DeleteData(updateBookView);
                    var bookview = _mapper.Map<BookView>(updateBookView);
                    AddData(bookview, userID, book.Id);
                    return RedirectToAction("Index", "Book");
                }
                catch (Exception e)
                {
                    _tableLogService.Discard();
                    _tableLogService.Update(DataUtil.BookTableName, book.Id, DataUtil.TableStatusError, e.Message);
                    ViewBag.ErrorMessage = DataUtil.DoNotSaved;
                    return View(updateBookView);
                }
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
                try
                {
                    UpdateBookView bookview = CreateData(id, book);
                    DeleteData(bookview);
                    _bookService.Delete(book);
                    _bookService.Save();
                    _tableLogService.Delete(DataUtil.BookTableName, book.Id, DataUtil.TableStatusInfo, DataUtil.DeleteData);
                    return ResultApi.Succeeded();
                }
                catch (Exception e)
                {
                    _tableLogService.Discard();
                    _tableLogService.Update(DataUtil.BookTableName, book.Id, DataUtil.TableStatusError, e.Message);
                    return ResultApi.Failed();
                }
            }
            else
            {
                _tableLogService.AddDataError(DataUtil.TableStatusError, DataUtil.DataDoMotFound, null);
                return ResultApi.Failed();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddBook(BookView bookView)
        {
            
            if (ModelState.IsValid)
            {
                
                try
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Image", "Book");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    string filePath = Path.Combine(uploadsFolder, bookView.ImageFile.FileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        bookView.ImageFile.CopyTo(fileStream);
                    }
                    string path = Path.Combine(DataUtil.BookImagepath, bookView.ImageFile.FileName);
                    int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    var book = new Book
                    {
                        Name = bookView.Name,
                        Description = bookView.Description,
                        ImagePath = path,
                    };
                    _bookService.Add(book);
                    _bookService.Save();
                    _tableLogService.AddData(DataUtil.BookTableName, book.Id, DataUtil.TableStatusInfo, DataUtil.NewData, userID);
                    AddData(bookView, userID, book.Id);
                    return RedirectToAction("Index", "Book");
                }
                catch (Exception e)
                {
                    _tableLogService.Discard();
                    _tableLogService.AddDataError(DataUtil.TableStatusError, e.Message, null);
                    return ResultApi.Failed();
                }
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
                var data  = _authorBookService.AddData(i, id);
                _tableLogService.AddData(DataUtil.AuthorBookTableName, data.Id, DataUtil.TableStatusInfo, DataUtil.NewData, userID);

            }
            foreach (int i in bookView.GenreId)
            {
                var data = _bookGenreService.AddData(i, id);
                _tableLogService.AddData(DataUtil.BookGenreTableName, data.Id, DataUtil.TableStatusInfo, DataUtil.NewData, userID);
            }
            foreach (int i in bookView.LanguageId)
            {
                var data =  _bookLanguageService.AddData(i, id);
                _tableLogService.AddData(DataUtil.BookLanguageTableName, data.Id, DataUtil.TableStatusInfo, DataUtil.NewData, userID);
            }
            foreach (int i in bookView.PublisherId)
            {
                var data = _bookPublisherService.AddData(i, id);
                _tableLogService.AddData(DataUtil.BookPublisherTableName, data.Id, DataUtil.TableStatusInfo, DataUtil.NewData, userID);
            }
            foreach (int i in bookView.StorageId)
            {
                var data = _bookStorageService.AddData(i, id);
                _tableLogService.AddData(DataUtil.BookStorageTableName, data.Id, DataUtil.TableStatusInfo, DataUtil.NewData, userID);

            }
        }

        private void DeleteData(UpdateBookView bookView)
        {
            var genres = _bookGenreService.GetByBookId(bookView.Id);
            foreach (var genre in genres)
            {
                _bookGenreService.Delete(genre);
                _bookGenreService.Save();
                _tableLogService.Delete(DataUtil.BookGenreTableName, genre.Id, DataUtil.TableStatusInfo, DataUtil.DeleteData);

            }
            var languages = _bookLanguageService.GetByBookId(bookView.Id);
            foreach (var language in languages)
            {
                _bookLanguageService.Delete(language);
                _bookGenreService.Save();
                _tableLogService.Delete(DataUtil.BookLanguageTableName, language.Id, DataUtil.TableStatusInfo, DataUtil.DeleteData);

            }
            var publishers = _bookPublisherService.GetByBookId(bookView.Id);
            foreach (var publisher in publishers)
            {

                _bookPublisherService.Delete(publisher);
                _bookPublisherService.Save();
                _tableLogService.Delete(DataUtil.BookPublisherTableName, publisher.Id, DataUtil.TableStatusInfo, DataUtil.DeleteData);

            }
            var storages = _bookStorageService.GetByBookId(bookView.Id);
            foreach (var storage in storages)
            {
                _bookStorageService.Delete(storage);
                _bookStorageService.Save();
                _tableLogService.Delete(DataUtil.BookStorageTableName, storage.Id, DataUtil.TableStatusInfo, DataUtil.DeleteData);

            }
            var authors = _authorBookService.GetByBookId(bookView.Id);
            foreach (var author in authors)
            {
                _authorBookService.Delete(author);
                _authorBookService.Save();
                _tableLogService.Delete(DataUtil.AuthorBookTableName, author.Id, DataUtil.TableStatusInfo, DataUtil.DeleteData);

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
