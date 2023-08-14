using LibraryService;
using LibrarySystem.Data;
<<<<<<< HEAD
using LibrarySystem.Models.View;
=======
using LibrarySystem.Models.Report;
>>>>>>> rdlc
using LibrarySystem.Util;
using LibrarySystemModels.Parameters;
using LibrarySystemModels.Procedure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
<<<<<<< HEAD
=======
using System.Data;
using Microsoft.Reporting.NETCore;
>>>>>>> rdlc

namespace LibrarySystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly ITableLogService _tableLogService;
<<<<<<< HEAD
        public ReportController(IReportService reportService, ITableLogService tableLogService)
        {
            _reportService = reportService;
            _tableLogService = tableLogService;
=======
        private readonly IBookGenreService _bookGenreService;
        private readonly IGenresService _genresService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ReportController(IReportService reportService, ITableLogService tableLogService,
            IBookGenreService bookGenreService, IGenresService genresService, IWebHostEnvironment webHostEnvironment)
        {
            _reportService = reportService;
            _tableLogService = tableLogService;
            _bookGenreService = bookGenreService;
            _genresService = genresService;
            _webHostEnvironment = webHostEnvironment;
>>>>>>> rdlc
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

        public ActionResult GeneratePopularityCsv(ByPopularityParameters parameters)
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var data = _reportService.GetByPopularities(parameters);
                var csvBytes = GenerateFiles.GenerateCsvBytes(data);
                return File(csvBytes, "text/csv", "models_list.csv");
            }
            catch (Exception e)
            {
                _tableLogService.Discard();
                _tableLogService.AddDataError(DataUtil.TableStatusError, e.Message, userID);
                return ResultApi.Failed();
            }
        }

        public ActionResult GenerateCurrentTransactionsCsv(CurrentTransactionsParameters parameters)
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var data = _reportService.GetCurrentTransactions(parameters);
                var csvBytes = GenerateFiles.GenerateCsvBytes(data);
                return File(csvBytes, "text/csv", "models_list.csv");
            }
            catch (Exception e)
            {
                _tableLogService.Discard();
                _tableLogService.AddDataError(DataUtil.TableStatusError, e.Message, userID);
                return ResultApi.Failed();
            }
        }

        public ActionResult GenerateOverdueTransactionsCsv(OverdueTransactionsParameters parameters)
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var data = _reportService.GetOverdueTransactions(parameters);
                var csvBytes = GenerateFiles.GenerateCsvBytes(data);
                return File(csvBytes, "text/csv", "models_list.csv");
            }
            catch (Exception e)
            {
                _tableLogService.Discard();
                _tableLogService.AddDataError(DataUtil.TableStatusError, e.Message, userID);
                return ResultApi.Failed();
            }
        }

        public ActionResult GenerateClientsPerformanceCsv(ClientsPerformanceParameters parameters)
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var data = _reportService.GetClientsPerformance(parameters);
                var csvBytes = GenerateFiles.GenerateCsvBytes(data);
                return File(csvBytes, "text/csv", "models_list.csv");
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
<<<<<<< HEAD
=======

        public IActionResult GeneratePDF()
        {
            try
            {
                var reportPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "Reports", "GenreReport.rdlc");

                var bookGenres = _bookGenreService.GetAll();
                var genres = _genresService.GetAll();
                var totalBookGenresCount = bookGenres.Count;
                var reportData = genres.Select(genre => new
                {
                    Id = genre.Id,
                    Name = genre.Name,
                    Count = (int)(genre.BookGenres.Count(bg => bookGenres.Any(bg2 => bg2.GenreId == genre.Id)) / (double)totalBookGenresCount * 100),
                }).ToList();

                var reportDataTable = new DataTable("DataSetName");

                reportDataTable.Columns.Add("Id", typeof(int));
                reportDataTable.Columns.Add("Name", typeof(string));
                reportDataTable.Columns.Add("Count", typeof(int));

                foreach (var item in reportData)
                {
                    reportDataTable.Rows.Add(item.Id, item.Name, item.Count);
                }

                var localReport = new LocalReport();
                localReport.ReportPath = reportPath;
                var rds = new ReportDataSource();
                rds.Name = "ReportData";
                rds.Value = reportData;
                localReport.DataSources.Add(rds);

                var result = localReport.Render("pdf");

                return File(result, "application/pdf");
            }
            catch (Exception e)
            {
                var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _tableLogService.Discard();
                _tableLogService.AddDataError(DataUtil.TableStatusError, e.Message, userID);
                return RedirectToAction("Genres", "BookCategory");
            }
        }
>>>>>>> rdlc
    }
}
