using AutoMapper;
using LibraryService;
using LibrarySystem.Data;
using LibrarySystem.Models.Api;
using LibrarySystem.Models.View;
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

        public BookController(IGenresService genresService, ITableLogService tableLogService,
            IMapper mapper, IPublisherService publisherService)
        {
            _genresService = genresService;
            _tableLogService = tableLogService;
            _mapper = mapper;
            _publisherService = publisherService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Genres()
        {
            var genres = _genresService.GetAll();
            return View(new GenresView { Genres = genres });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Publishers()
        {
            var genres = _publisherService.GetAll();
            return View(new PublisherView {Publishers = genres});
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
                return new ContentResult
                {
                    Content = genre.Id.ToString(),
                    ContentType = "text/plain",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            else
            {
                return new ContentResult
                {
                    Content = "model is not valid",
                    ContentType = "text/plain",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
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

                return new ContentResult
                {
                    Content = "true",
                    ContentType = "text/plain",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            else
            {
                return new ContentResult
                {
                    Content = "false",
                    ContentType = "text/plain",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
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
                    return new ContentResult
                    {
                        Content = "true",
                        ContentType = "text/plain",
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
                else
                {
                    return new ContentResult
                    {
                        Content = "false",
                        ContentType = "text/plain",
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };

                }
            }
            else
            {
                return new ContentResult
                {
                    Content = "model is not valid",
                    ContentType = "text/plain",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
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
                return new ContentResult
                {
                    Content = publisher.Id.ToString(),
                    ContentType = "text/plain",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            else
            {
                return new ContentResult
                {
                    Content = "model is not valid",
                    ContentType = "text/plain",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
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
                    return new ContentResult
                    {
                        Content = "true",
                        ContentType = "text/plain",
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
                else
                {
                    return new ContentResult
                    {
                        Content = "false",
                        ContentType = "text/plain",
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };

                }
            }
            else
            {
                return new ContentResult
                {
                    Content = "model is not valid",
                    ContentType = "text/plain",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult DeletePublisher(int id)
        {
            Console.WriteLine(id);
            var publisher = _publisherService.GetById(id);
            if (publisher != null)
            {
                _publisherService.Delete(publisher);
                _publisherService.Save();
                _tableLogService.Delete(publisher.LogsId);
                _tableLogService.Save();

                return new ContentResult
                {
                    Content = "true",
                    ContentType = "text/plain",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            else
            {
                return new ContentResult
                {
                    Content = "false",
                    ContentType = "text/plain",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }
    }
}
