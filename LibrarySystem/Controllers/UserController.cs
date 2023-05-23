using LibraryService;
using LibrarySystem.Models.View;
using LibrarySystemModels;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        public IActionResult Index()
        {
            var allUser = _userService.GetAllWithPerson();
            var allRole = _roleService.GetRoles();
            return View(new UsersView { Users = allUser,Roles = allRole});
        }
    }
}
