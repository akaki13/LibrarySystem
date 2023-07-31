using LibraryService;
using LibrarySystem.Data;
using LibrarySystem.Models.View;
using LibrarySystem.Util;
using LibrarySystemModels.Parameters;
using LibrarySystemModels.Procedure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibrarySystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly ITableLogService _tableLogService;
        public ReportController(IReportService reportService, ITableLogService tableLogService)
        {
            _reportService = reportService;
            _tableLogService = tableLogService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OverdueTransaction()
        {
            return View();
        }

        public IActionResult CurrentTransactions()
        {
            return View();
        }

        public IActionResult ByPopularity()
        {
            return View();
        }

        public IActionResult ClientsPerformance()
        {
            return View();
        }

        public ActionResult<List<OverdueTransactions>> GetOverdueTransaction(OverdueTransactionsParameters parameters)
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                return _reportService.GetOverdueTransactions(parameters);
            }
            catch (Exception e)
            {
                _tableLogService.Discard();
                _tableLogService.AddDataError(DataUtil.TableStatusError, e.Message, userID);
                return ResultApi.Failed();
            }
        }

        public ActionResult<List<CurrentTransactions>> GetCurrentTransactions(CurrentTransactionsParameters parameters)
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                return _reportService.GetCurrentTransactions(parameters);
            }
            catch (Exception e)
            {
                _tableLogService.Discard();
                _tableLogService.AddDataError(DataUtil.TableStatusError, e.Message, userID);
                return ResultApi.Failed();
            }
        }

        public ActionResult GeneratePopularityPdf(ByPopularityParameters parameters)
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var data = _reportService.GetByPopularities(parameters);
                var pdfBytes = GenerateFiles.GeneratePdfBytes(data);
                return File(pdfBytes, "application/pdf", "models_list.pdf");
            }
            catch (Exception e)
            {
                _tableLogService.Discard();
                _tableLogService.AddDataError(DataUtil.TableStatusError, e.Message, userID);
                return ResultApi.Failed();
            }
        }

        public ActionResult GenerateCurrentTransactionsPdf(CurrentTransactionsParameters parameters)
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var data = _reportService.GetCurrentTransactions(parameters);
                var pdfBytes = GenerateFiles.GeneratePdfBytes(data);
                return File(pdfBytes, "application/pdf", "models_list.pdf");
            }
            catch (Exception e)
            {
                _tableLogService.Discard();
                _tableLogService.AddDataError(DataUtil.TableStatusError, e.Message, userID);
                return ResultApi.Failed();
            }
        }

        public ActionResult GenerateOverdueTransactionsPdf(OverdueTransactionsParameters parameters)
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var data = _reportService.GetOverdueTransactions(parameters);
                var pdfBytes = GenerateFiles.GeneratePdfBytes(data);
                return File(pdfBytes, "application/pdf", "models_list.pdf");
            }
            catch (Exception e)
            {
                _tableLogService.Discard();
                _tableLogService.AddDataError(DataUtil.TableStatusError, e.Message, userID);
                return ResultApi.Failed();
            }
        }

        public ActionResult GenerateClientsPerformancePdf(ClientsPerformanceParameters parameters)
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var data = _reportService.GetClientsPerformance(parameters);
                var pdfBytes = GenerateFiles.GeneratePdfBytes(data);
                return File(pdfBytes, "application/pdf", "models_list.pdf");
            }
            catch (Exception e)
            {
                _tableLogService.Discard();
                _tableLogService.AddDataError(DataUtil.TableStatusError, e.Message, userID);
                return ResultApi.Failed();
            }
        }

        public ActionResult<List<ByPopularity>> GetByPopularity(ByPopularityParameters parameters)
        {
            
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                return _reportService.GetByPopularities(parameters);
            }
            catch (Exception e)
            {
                _tableLogService.Discard();
                _tableLogService.AddDataError(DataUtil.TableStatusError, e.Message, userID);
                return ResultApi.Failed();
            }
        }

        public ActionResult<List<ClientsPerformance>> GetClientsPerformance(ClientsPerformanceParameters parameters)
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                return _reportService.GetClientsPerformance(parameters);
            }
            catch (Exception e)
            {
                _tableLogService.Discard();
                _tableLogService.AddDataError(DataUtil.TableStatusError, e.Message, userID);
                return ResultApi.Failed();
            }
        }
    }
}
