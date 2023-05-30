using LibraryService;
using LibrarySystem.Models.View;
using LibrarySystemModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
           
            var allRole = _roleService.GetRoles();
            return View(new UsersView {  Roles = allRole });
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
