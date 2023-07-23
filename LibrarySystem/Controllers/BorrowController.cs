using AutoMapper;
using LibraryService;
using LibrarySystem.Data;
using LibrarySystem.Models.Api;
using LibrarySystem.Models.View;
using LibrarySystem.Util;
using LibrarySystemModels;
using LibrarySystemModels.Procedure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Data;
using System.Security.Claims;

namespace LibrarySystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BorrowController : Controller
    {
        private readonly IBorrowService _borrowService;
        private readonly IBookService _bookService;
        private readonly IPersonService _personService;
        private readonly ITableLogService _tableLogService;
        private readonly IMapper _mapper;

        public BorrowController(IBorrowService borrowService, IBookService bookService, IPersonService personService,
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

        public ActionResult<List<Borrow>> GetBorrow()
        {
            return _borrowService.GetAll();
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

        [HttpDelete]
        public ActionResult DeleteBorrow(int id)
        {
            var borrow = _borrowService.GetById(id);
            if (borrow != null)
            {
                try
                {
                    _borrowService.Delete(borrow);
                    _borrowService.Save();
                    _tableLogService.Delete(DataUtil.BorrowTableName, borrow.Id, DataUtil.TableStatusInfo, DataUtil.DeleteData);
                    return ResultApi.Succeeded();
                }
                catch (Exception e)
                {
                    _tableLogService.Discard();
                    _tableLogService.Update(DataUtil.PublisherTableName, borrow.Id, DataUtil.TableStatusError, e.Message);
                    return ResultApi.Failed();
                }
            }
            else
            {
                _tableLogService.AddDataError(DataUtil.TableStatusError, DataUtil.DataDoMotFound, null);
                return ResultApi.Failed();
            }
        }

        [HttpPost]
        public ActionResult BookReturned(int id)
        {
            var borrow = _borrowService.GetById(id);
            if (borrow != null)
            {
                try
                {
                    borrow.ActualReturnedTime = DateTime.Now;
                    _borrowService.Update(borrow);
                    _borrowService.Save();
                    _tableLogService.Update(DataUtil.BorrowTableName, borrow.Id, DataUtil.TableStatusInfo, DataUtil.UpdateData);
                    return ResultApi.Succeeded();
                }
                catch (Exception e)
                {
                    _tableLogService.Discard();
                    _tableLogService.Update(DataUtil.PublisherTableName, borrow.Id, DataUtil.TableStatusError, e.Message);
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
