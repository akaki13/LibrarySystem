using LibraryService;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
    public class BorrowController : Controller
    {
        private readonly IBorrowService _borrowService;
        private readonly IBookService _bookService;
        private readonly IPersonService _personService;
        private readonly ITableLogService _tableLogService;

        public BorrowController(IBorrowService borrowService,IBookService bookService, IPersonService personService, ITableLogService tableLogService)
        {
            _tableLogService = tableLogService;
            _borrowService = borrowService;
            _bookService = bookService;
            _personService = personService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddBorrow() 
        {
            return View();
        }
    }
}
