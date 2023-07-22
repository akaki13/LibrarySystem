using LibraryService;
using LibrarySystemModels.Procedure;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
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

        public ActionResult<List<OverdueTransactions>> GetOverdueTransaction()
        {
            return _reportService.GetOverdueTransactions();
        }

        public ActionResult<List<CurrentTransactions>> GetCurrentTransactions()
        {
            return _reportService.GetCurrentTransactions();
        }

        public ActionResult<List<ByPopularity>> GetByPopularity()
        {
            return _reportService.GetByPopularities();
        }

        public ActionResult<List<ClientsPerformance>> GetClientsPerformance()
        {
            return _reportService.GetClientsPerformance();
        }

    }
}
