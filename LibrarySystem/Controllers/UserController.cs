using LibraryService;
using LibrarySystem.Models.View;
using LibrarySystemModels;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;

namespace LibrarySystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IRoleUserService _roleUserService;
        private readonly IPersonService _personService;

        public UserController(IUserService userService, IRoleService roleService, IPersonService personService, IRoleUserService roleUserService)
        {
            _userService = userService;
            _roleService = roleService;
            _personService = personService;
            _roleUserService = roleUserService;
        }
        public IActionResult Index()
        {
           
            var allRole = _roleService.GetRoles();
            return View(new UsersView {  Roles = allRole });
        }

        [HttpGet]
        public ActionResult<List<User>> Users()
        {
            var allUser = _userService.GetAll();
            return allUser;
        }

        [HttpGet]
        public ActionResult<List<Role>> Roles()
        {
            var allRole = _roleService.GetRoles();
            return allRole;
        }

        public ActionResult<List<RoleUser>> RolesUser()
        {
            var allRole = _roleUserService.GetAll();
            return allRole;
        }

        public ActionResult<List<Person>> Person()
        {
            var allperson = _personService.GetAll();
            return allperson;
        }

    }
}
