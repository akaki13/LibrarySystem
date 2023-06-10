using AutoMapper;
using LibraryService;
using LibrarySystem.Data;
using LibrarySystem.Models.Api;
using LibrarySystem.Models.View;
using LibrarySystem.Util;
using LibrarySystemModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace LibrarySystem.Controllers
{
    public class BookController : Controller
    {
        private readonly IGenresService _genresService;
        private readonly ITableLogService _tableLogService;
        private readonly IMapper _mapper;
        private readonly IPublisherService _publisherService;
        private readonly ILanguageService _languageService;
        private readonly IAuthorService _authorService;
        private readonly IStorageService _storageService;

        public BookController(IGenresService genresService, ITableLogService tableLogService,
            IMapper mapper, IPublisherService publisherService, ILanguageService languageService,
            IAuthorService authorService, IStorageService storageService)
        {
            _genresService = genresService;
            _tableLogService = tableLogService;
            _mapper = mapper;
            _publisherService = publisherService;
            _languageService = languageService;
            _authorService = authorService;
            _storageService = storageService;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult Genres()
        {
           
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Authors()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Storage()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Publishers()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<Publisher>> GetPublishers()
        {
            var publishers = _publisherService.GetAll();
            return publishers;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<Genre>> GetGenres()
        {
            var genres = _genresService.GetAll();
            return genres;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddGenre(AddGenreApi addGenreApi)
        {
            if (ModelState.IsValid)
            {
                var genre = _mapper.Map<Genre>(addGenreApi);
                int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var tableLog = _tableLogService.AddWithId(DataUtil.GenreTableName, userID);
                _tableLogService.Save();
                genre.LogsId = tableLog.Id;
                _genresService.Add(genre);
                _genresService.Save();
                return ResultApi.CreateData(genre.Id);
            }
            else
            {
                return ResultApi.ModelNotValid();
                
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult DeleteGenre(int id)
        {
            Console.WriteLine(id);
            var genre = _genresService.GetById(id);
            if (genre != null)
            {
                _genresService.Delete(genre);
                _genresService.Save();
                _tableLogService.Delete(genre.LogsId);
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
        public ActionResult UpdateGenre(UpdateGenreApi updateGenreApi)
        {
            if (ModelState.IsValid)
            {
                var genre = _genresService.GetById(updateGenreApi.Id);
                if (genre != null)
                {
                    _mapper.Map(updateGenreApi, genre);
                    _genresService.Update(genre);
                    _genresService.Save();
                    _tableLogService.Update(genre.LogsId);
                    _tableLogService.Save();
                    return ResultApi.Succeeded();
                }
                else
                {
                    return ResultApi.Failed();

                }
            }
            else
            {
                return ResultApi.ModelNotValid();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddPublisher(AddPublisherApi addPublisherApi)
        {
            if (ModelState.IsValid)
            {
                var publisher = _mapper.Map<Publisher>(addPublisherApi);
                int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var tableLog = _tableLogService.AddWithId(DataUtil.PublisherTableName, userID);
                _tableLogService.Save();
                publisher.LogsId = tableLog.Id;
                _publisherService.Add(publisher);
                _publisherService.Save();
                return ResultApi.CreateData(publisher.Id);
            }
            else
            {
                return ResultApi.ModelNotValid();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult UpdatePublisher(UpdatePublisherApi updatePublisherApi)
        {
            if (ModelState.IsValid)
            {
                var publisher = _publisherService.GetById(updatePublisherApi.Id);
                if (publisher != null)
                {
                    _mapper.Map(updatePublisherApi, publisher);
                    _publisherService.Update(publisher);
                    _publisherService.Save();
                    _tableLogService.Update(publisher.LogsId);
                    _tableLogService.Save();
                    return ResultApi.Succeeded();
                }
                else
                {
                    return ResultApi.Failed();

                }
            }
            else
            {
                return ResultApi.ModelNotValid();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult DeletePublisher(int id)
        {
            var publisher = _publisherService.GetById(id);
            if (publisher != null)
            {
                _publisherService.Delete(publisher);
                _publisherService.Save();
                _tableLogService.Delete(publisher.LogsId);
                _tableLogService.Save();
                return ResultApi.Succeeded();
            }
            else
            {
                return ResultApi.Failed();
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Language()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<Language>> GetLanguage()
        {
            var languages = _languageService.GetAll();
            return languages;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult DeleteLanguage(int id)
        {
            var language = _languageService.GetById(id);
            if (language != null)
            {
                _languageService.Delete(language);
                _publisherService.Save();
                _tableLogService.Delete(language.LogsId);
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
        public ActionResult UpdateLanguage(UpdateLanguageApi updateLanguageApi)
        {
            if (ModelState.IsValid)
            {
                var language = _languageService.GetById(updateLanguageApi.Id);
                if (language != null)
                {
                    _mapper.Map(updateLanguageApi, language);
                    _languageService.Update(language);
                    _languageService.Save();
                    _tableLogService.Update(language.LogsId);
                    _tableLogService.Save();
                    return ResultApi.Succeeded();
                }
                else
                {
                    return ResultApi.Failed();

                }
            }
            else
            {
                return ResultApi.ModelNotValid();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddLanguage(AddLanguageApi addLanguageApi)
        {
            if (ModelState.IsValid)
            {
                var language = _mapper.Map<Language>(addLanguageApi);
                int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var tableLog = _tableLogService.AddWithId(DataUtil.LanguageTableName, userID);
                _tableLogService.Save();
                language.LogsId = tableLog.Id;
                _languageService.Add(language);
                _languageService.Save();
                return ResultApi.CreateData(language.Id);
            }
            else
            {
                return ResultApi.ModelNotValid();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<Author>> GetAuthor()
        {
            var authors = _authorService.GetAll();
            return authors;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult DeleteAuthor(int id)
        {
            var author = _authorService.GetById(id);
            if (author != null)
            {
                _authorService.Delete(author);
                _authorService.Save();
                _tableLogService.Delete(author.LogsId);
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
        public ActionResult UpdateAuthor(UpdateAuthorApi updateAuthorApi)
        {
            if (ModelState.IsValid)
            {
                var author = _authorService.GetById(updateAuthorApi.Id);
                if (author != null)
                {
                    _mapper.Map(updateAuthorApi, author);
                    _authorService.Update(author);
                    _authorService.Save();
                    _tableLogService.Update(author.LogsId);
                    _tableLogService.Save();
                    return ResultApi.Succeeded();
                }
                else
                {
                    return ResultApi.Failed();

                }
            }
            else
            {
                return ResultApi.ModelNotValid();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddAuthor(AddAuthorApi addAuthorApi)
        {
            if (ModelState.IsValid)
            {
                var author = _mapper.Map<Author>(addAuthorApi);
                int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var tableLog = _tableLogService.AddWithId(DataUtil.AuthorTableName, userID);
                _tableLogService.Save();
                author.LogsId = tableLog.Id;
                _authorService.Add(author);
                _authorService.Save();
                return ResultApi.CreateData(author.Id);
            }
            else
            {
                return ResultApi.ModelNotValid();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<Storage>> GetStorage()
        {
            var storages = _storageService.GetAll();
            return storages;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult DeleteStorage(int id)
        {
            var storage = _storageService.GetById(id);
            if (storage != null)
            {
                _storageService.Delete(storage);
                _storageService.Save();
                _tableLogService.Delete(storage.LogsId);
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
        public ActionResult UpdateStorage(UpdateStorageApi updateStorageApi)
        {
            if (ModelState.IsValid)
            {

                var storage = _storageService.GetById(updateStorageApi.Id);
                if (storage != null)
                {
                    _mapper.Map(updateStorageApi, storage);
                    _storageService.Update(storage);
                    _storageService.Save();
                    _tableLogService.Update(storage.LogsId);
                    _tableLogService.Save();
                    return ResultApi.Succeeded();
                }
                else
                {
                    return ResultApi.Failed();

                }
            }
            else
            {
                return ResultApi.ModelNotValid();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddStorage(AddStorageApi addStorageApi)
        {
            if (ModelState.IsValid)
            {
                var storage = _mapper.Map<Storage>(addStorageApi);
                int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var tableLog = _tableLogService.AddWithId(DataUtil.StorageTableName, userID);
                _tableLogService.Save();
                storage.LogsId = tableLog.Id;
                _storageService.Add(storage);
                _storageService.Save();
                return ResultApi.CreateData(storage.Id);
                
            }
            else
            {
                return ResultApi.ModelNotValid();
            }
        }
    }
}
