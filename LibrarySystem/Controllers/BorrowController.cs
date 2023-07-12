using AutoMapper;
using LibraryService;
using LibrarySystem.Data;
using LibrarySystem.Models.View;
using LibrarySystem.Util;
using LibrarySystemModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibrarySystem.Controllers
{
    public class BorrowController : Controller
    {
        private readonly IBorrowService _borrowService;
        private readonly IBookService _bookService;
        private readonly IPersonService _personService;
        private readonly ITableLogService _tableLogService;
        private readonly IMapper _mapper;

        public BorrowController(IBorrowService borrowService,IBookService bookService, IPersonService personService,
            ITableLogService tableLogService, IMapper mapper)
        {
            _tableLogService = tableLogService;
            _borrowService = borrowService;
            _bookService = bookService;
            _personService = personService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddBorrow() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBorrow(AddBorrowView addBorrowView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    var borrow = _mapper.Map<Borrow>(addBorrowView);
                    borrow.TakeTime = DateTime.Now;
                    _borrowService.Add(borrow);
                    _borrowService.Save();
                    _tableLogService.AddData(DataUtil.BorrowTableName, borrow.Id, DataUtil.TableStatusInfo, DataUtil.NewData, userID);
                    return View();
                }
                catch (Exception ex)
                {
                    _tableLogService.Discard();
                    _tableLogService.AddDataError(DataUtil.TableStatusError, ex.Message, null);
                    ViewBag.ErrorMessage = DataUtil.DoNotSaved;
                    return View(addBorrowView);
                }
            }
            else
            {
                return View(addBorrowView);
            }
        }

    }
}
