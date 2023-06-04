using AutoMapper;
using LibraryService;
using LibrarySystem.Data;
using LibrarySystem.Models.Api;
using LibrarySystem.Models.View;
using LibrarySystemModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Protocol.Plugins;
using System;
using System.Net;
using System.Security.Claims;

namespace LibrarySystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IRoleUserService _roleUserService;
        private readonly IPersonService _personService;
        private readonly IPositionService _positionService;
        private readonly ITableLogService _tableLogService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IRoleService roleService, IPersonService personService,
            IRoleUserService roleUserService, IPositionService positionService, ITableLogService tableLogService,
            IMapper mapper)
        {
            _userService = userService;
            _roleService = roleService;
            _personService = personService;
            _roleUserService = roleUserService;
            _positionService = positionService;
            _tableLogService = tableLogService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {

            var allRole = _roleService.GetRoles();
            return View(new UsersView { Roles = allRole });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UserProfile(int id)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var user = _userService.GetWithPerson(id);
            if (user != null)
            {
                return View(new UserProfileView { User = user });
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Positions()
        {
            var positions = _positionService.GetAll();
            return View(new PositionsViews { Positions = positions });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddPosition(PositionApi position)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(position.Title);
                var savePosition = _mapper.Map<Position>(position);
                int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var tableLog = _tableLogService.AddWithId(DataUtil.PositionTableName, userID);
                _tableLogService.Save();
                savePosition.LogsId = tableLog.Id;
                _positionService.Add(savePosition);
                _positionService.Save();
                Console.WriteLine(savePosition.Id.ToString());
                return new ContentResult
                {
                    Content = savePosition.Id.ToString(),
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
        public ActionResult UpdatePosition(UpdatePositionApi position)
        {
            Console.WriteLine(position);
            if (ModelState.IsValid)
            {

                var updatePosition = _positionService.GetById(position.Id);
                if (updatePosition != null)
                {
                    _mapper.Map(position, updatePosition);
                     _positionService.Update(updatePosition);
                     _positionService.Save();
                     _tableLogService.Update(updatePosition.LogsId);
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
        public ActionResult DeletePosition(int  id)
        {
            Console.WriteLine(id);
            var position = _positionService.GetById(id);
            if(position != null)
            {
                _tableLogService.Delete(position.LogsId);
                _tableLogService.Save();
                _positionService.Delete(position);
                _positionService.Save();

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
        [HttpGet]
        public ActionResult<List<User>> Users()
        {
            var allUser = _userService.GetAll();
            return allUser;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<Role>> Roles()
        {
            var allRole = _roleService.GetRoles();
            return allRole;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<RoleUser>> RolesUser()
        {
            var allRole = _roleUserService.GetAll();
            return allRole;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<Person>> Person()
        {
            var allperson = _personService.GetAll();
            return allperson;
        }

    }
}
