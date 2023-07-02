using AutoMapper;
using LibraryService;
using LibrarySystem.Data;
using LibrarySystem.Models.Api;
using LibrarySystem.Util;
using LibrarySystemModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace LibrarySystem.Controllers
{
    public class BookCategoryController : Controller
    {
        private readonly IGenresService _genresService;
        private readonly ITableLogService _tableLogService;
        private readonly IMapper _mapper;
        private readonly IPublisherService _publisherService;
        private readonly ILanguageService _languageService;
        private readonly IAuthorService _authorService;
        private readonly IStorageService _storageService;

        public BookCategoryController(IGenresService genresService, ITableLogService tableLogService,
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
                _genresService.Add(genre);
                _genresService.Save();
                _tableLogService.AddData(DataUtil.GenreTableName, genre.Id, DataUtil.TableStatusInfo, DataUtil.NewData, userID);
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
                try
                {
                    _genresService.Delete(genre);
                    _genresService.Save();
                    _tableLogService.Delete(DataUtil.GenreTableName, genre.Id, DataUtil.TableStatusInfo, DataUtil.DeleteData);
                    return ResultApi.Succeeded();
                }
                catch (Exception e)
                {
                    _tableLogService.Discard();
                    _tableLogService.Update(DataUtil.GenreTableName, genre.Id, DataUtil.TableStatusError, e.Message);
                    return ResultApi.Failed();
                }
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
                    _tableLogService.Update(DataUtil.GenreTableName, genre.Id, DataUtil.TableStatusInfo, DataUtil.UpdateData);
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
                _publisherService.Add(publisher);
                _publisherService.Save();
                _tableLogService.AddData(DataUtil.PublisherTableName, publisher.Id, DataUtil.TableStatusInfo, DataUtil.NewData, userID);
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
                    _tableLogService.Update(DataUtil.PublisherTableName, publisher.Id, DataUtil.TableStatusInfo, DataUtil.UpdateData);

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
                try
                {
                    _publisherService.Delete(publisher);
                    _publisherService.Save();
                    _tableLogService.Delete(DataUtil.PublisherTableName, publisher.Id, DataUtil.TableStatusInfo, DataUtil.DeleteData);
                    return ResultApi.Succeeded();
                }
                catch (Exception e)
                {
                    _tableLogService.Discard();
                    _tableLogService.Update(DataUtil.PublisherTableName, publisher.Id, DataUtil.TableStatusError, e.Message);
                    return ResultApi.Failed();
                }
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
                try
                {
                    _languageService.Delete(language);
                    _publisherService.Save();
                    _tableLogService.Delete(DataUtil.LanguageTableName, language.Id, DataUtil.TableStatusInfo, DataUtil.DeleteData);
                    return ResultApi.Succeeded();
                }
                catch (Exception e)
                {
                    _tableLogService.Discard();
                    _tableLogService.Update(DataUtil.LanguageTableName, language.Id, DataUtil.TableStatusError, e.Message);
                    return ResultApi.Failed();
                }
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
                    _tableLogService.Update(DataUtil.LanguageTableName, language.Id, DataUtil.TableStatusInfo, DataUtil.UpdateData);
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
                _languageService.Add(language);
                _languageService.Save();
                _tableLogService.AddData(DataUtil.LanguageTableName, language.Id, DataUtil.TableStatusInfo, DataUtil.NewData, userID);

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
                try
                {
                    _authorService.Delete(author);
                    _authorService.Save();
                    _tableLogService.Delete(DataUtil.AuthorTableName, author.Id, DataUtil.TableStatusInfo, DataUtil.UpdateData);
                    return ResultApi.Succeeded();
                }
                catch (Exception e)
                {
                    _tableLogService.Discard();
                    _tableLogService.Update(DataUtil.AuthorTableName, author.Id, DataUtil.TableStatusError, e.Message);
                    return ResultApi.Failed();
                }
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
                    _tableLogService.Update(DataUtil.AuthorTableName, author.Id, DataUtil.TableStatusInfo, DataUtil.UpdateData);
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
                _authorService.Add(author);
                _authorService.Save();
                _tableLogService.AddData(DataUtil.AuthorTableName, author.Id, DataUtil.TableStatusInfo, DataUtil.NewData, userID);
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
                try
                {
                    _storageService.Delete(storage);
                    _storageService.Save();
                    _tableLogService.Delete(DataUtil.StorageTableName, storage.Id, DataUtil.TableStatusInfo, DataUtil.DeleteData);
                    return ResultApi.Succeeded();
                }
                catch (Exception e)
                {
                    _tableLogService.Discard();
                    _tableLogService.Update(DataUtil.StorageTableName, storage.Id, DataUtil.TableStatusError, e.Message);
                    return ResultApi.Failed();
                }
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
                    _tableLogService.Update(DataUtil.StorageTableName, storage.Id, DataUtil.TableStatusInfo, DataUtil.UpdateData);
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
                _storageService.Add(storage);
                _storageService.Save();
                _tableLogService.AddData(DataUtil.StorageTableName, storage.Id, DataUtil.TableStatusInfo, DataUtil.NewData, userID);

                return ResultApi.CreateData(storage.Id);

            }
            else
            {
                return ResultApi.ModelNotValid();
            }
        }
        
    }
}
