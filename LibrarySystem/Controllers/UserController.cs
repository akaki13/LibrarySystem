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
            Console.WriteLine(allRole);
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
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddPerson()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Person()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddPosition(AddPositionApi position)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var savePosition = _mapper.Map<Position>(position);
                    int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    _positionService.Add(savePosition);
                    _positionService.Save();
                    _tableLogService.AddData(DataUtil.PositionTableName, savePosition.Id, DataUtil.TableStatusInfo, DataUtil.NewData, userID);
                    return ResultApi.CreateData(savePosition.Id);
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
                return ResultApi.ModelNotValid();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult UpdatePosition(UpdatePositionApi position)
        {
            if (ModelState.IsValid)
            {

                var updatePosition = _positionService.GetById(position.Id);
                if (updatePosition != null)
                {
                    try
                    {
                        _mapper.Map(position, updatePosition);
                        _positionService.Update(updatePosition);
                        _positionService.Save();
                        _tableLogService.Update(DataUtil.PositionTableName, updatePosition.Id, DataUtil.TableStatusInfo, DataUtil.UpdateData);
                        return ResultApi.Succeeded();
                    }
                    catch (Exception e)
                    {
                        _tableLogService.Discard();
                        _tableLogService.Update(DataUtil.PositionTableName, updatePosition.Id, DataUtil.TableStatusError, e.Message);
                        return ResultApi.Failed();
                    }
                }
                else
                {
                    _tableLogService.AddDataError(DataUtil.TableStatusError, DataUtil.DataDoMotFound, null);
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
        public ActionResult DeletePosition(int  id)
        {
            var position = _positionService.GetById(id);
            if(position != null)
            {
                try
                {
                    _positionService.Update(position);
                    _positionService.Save();
                    _tableLogService.Delete(DataUtil.PositionTableName, position.Id, DataUtil.TableStatusInfo, DataUtil.DeleteData);
                    return ResultApi.Succeeded();
                }
                catch (Exception e)
                {
                    _tableLogService.Discard();
                    _tableLogService.Update(DataUtil.PositionTableName, position.Id, DataUtil.TableStatusError, e.Message);
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
        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            var allUser = _userService.GetAll();
            return allUser;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<Role>> GetRoles()
        {
            var allRole = _roleService.GetRoles();
            return allRole;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<RoleUser>> GetRolesUser()
        {
            var allRole = _roleUserService.GetAll();
            return allRole;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<Person>> GetPerson()
        {
            var allperson = _personService.GetAll();
            return allperson;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<Position>> GetPosition()
        {
            var allposition = _positionService.GetAll();
            return allposition;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddPerson(AddPersonView personView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var emailCheck = _personService.GetByEmail(personView.Email);
                    if (emailCheck != null)
                    {
                        ViewBag.ErrorMessage = DataUtil.EmailExist;
                        return View(personView);
                    }
                    var person = _mapper.Map<Person>(personView);
                    int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    _personService.Add(person);
                    _personService.Save();
                    _tableLogService.AddData(DataUtil.PersonTableName, person.Id, DataUtil.TableStatusInfo, DataUtil.NewData, userID);
                    return RedirectToAction("Person", "User");
                }
                catch (Exception e)
                {
                    _tableLogService.Discard();
                    _tableLogService.AddDataError(DataUtil.TableStatusError, e.Message, null);
                    ViewBag.ErrorMessage = DataUtil.DoNotSaved;
                    return View(personView);
                }
            }
            else
            {
                return View(personView);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult UpdatePerson(int id)
        {
            var person = _personService.GetById(id);
            if (person != null)
            {
                var personView = _mapper.Map<UpdatePersonView>(person);
                return View(personView);
            }
            else
            {
                return RedirectToAction("Person", "User");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult UpdatePerson(UpdatePersonView updatePersonView)
        {
            if (ModelState.IsValid)
            {
                var person = _personService.GetById(updatePersonView.Id);
                if (person == null)
                {
                    _tableLogService.AddDataError(DataUtil.TableStatusError, DataUtil.DataDoMotFound, null);
                    ViewBag.ErrorMessage = DataUtil.DoNotSaved;
                    return View(updatePersonView);
                }
                var ckeckEmail = _personService.GetByEmail(updatePersonView.Email);
                if (ckeckEmail == null || ckeckEmail.Email == person.Email)
                {
                    try
                    {
                        _mapper.Map(updatePersonView, person);
                        _personService.Update(person);
                        _personService.Save();
                        _tableLogService.Update(DataUtil.PersonTableName, person.Id, DataUtil.TableStatusInfo, DataUtil.NewData);
                        return RedirectToAction("Person", "User");
                    }
                    catch (Exception e)
                    {
                        _tableLogService.Discard();
                        _tableLogService.AddDataError(DataUtil.TableStatusError, e.Message, null);
                        ViewBag.ErrorMessage = DataUtil.DoNotSaved;
                        return View(updatePersonView);
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = DataUtil.EmailExist;
                    return View(updatePersonView);
                }
            }
            else
            {
                return View(updatePersonView);
            }
           
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult DeletePerson(int id)
        {
            var person = _personService.GetById(id);
            if (person != null)
            {
                try
                {
                    _personService.Delete(person);
                    _personService.Save();
                    _tableLogService.Delete(DataUtil.PersonTableName, person.Id, DataUtil.TableStatusInfo, DataUtil.DeleteData);
                    return ResultApi.Succeeded();
                }
                catch (Exception e)
                {
                    _tableLogService.Discard();
                    _tableLogService.Update(DataUtil.PersonTableName, person.Id, DataUtil.TableStatusError, e.Message);
                    return ResultApi.Failed();
                }
            }
            else
            {
                _tableLogService.AddDataError(DataUtil.TableStatusError, DataUtil.DataDoMotFound, null);
                return ResultApi.Failed();
            }
        }
    }
}
