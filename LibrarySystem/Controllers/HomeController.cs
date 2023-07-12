using LibraryService;
using LibrarySystem.Data;
using LibrarySystem.Models;
using LibrarySystem.Models.View;
using LibrarySystem.Util;
using LibrarySystemData.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibrarySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISliderService _sliderService;

        public HomeController(ILogger<HomeController> logger,ISliderService sliderService)
        {
            _logger = logger;
            _sliderService = sliderService;
        }

        public IActionResult Index()
        {
            var sliders = _sliderService.GetAll();
            return View(new SliderView {Sliders = sliders});
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