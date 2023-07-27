using LibraryService;
using LibrarySystem.Data;
using LibrarySystem.Models;
using LibrarySystem.Models.View;
using LibrarySystem.Util;
using LibrarySystemData.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibrarySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, ISliderService sliderService, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _sliderService = sliderService;
            _webHostEnvironment = webHostEnvironment;

        }

        public IActionResult Index()
        {

            var sliders = _sliderService.GetAll();
            return View(new SliderView { Sliders = sliders });
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
        [HttpPost]
        public IActionResult Privacy(BookView model)
        {
            

            // Save the image file path in the database or perform other operations as needed
            // For simplicity, we are not doing that in this example.

            //return RedirectToAction("Upload", "Image");
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