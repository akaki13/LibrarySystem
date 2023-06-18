using LibraryService;
using LibrarySystem.Models;
using LibrarySystem.Util;
using LibrarySystemData.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibrarySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        public HomeController(ILogger<HomeController> logger,IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        /*		public IActionResult NavBar()
                {
                    var dad =JsonUtil.TakeBookData();
                    return PartialView(dad);
                }*/
        public IActionResult Privacy()
        {
         /*   var user = _userService.GetUser(1);
            Console.WriteLine(user.Login);*/
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}