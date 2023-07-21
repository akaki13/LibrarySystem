using LibrarySystemData.Repositories;
using LibrarySystemModels;
using LibrarySystemModels.Procedure;

namespace LibraryService
{
    public class ReportService : IReportService
    {
        public readonly IReportRepository _reportRepository;
        
        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public List<ByPopularity> GetByPopularities()
        {
            return _reportRepository.GetByPopularities();

        }

        public List<ClientsPerformance> GetClientsPerformance()
        {
            return _reportRepository.GetClientsPerformance();
        }

        public List<OverdueTransactions> GetOverdueTransactions()
        {
            return _reportRepository.GetOverdueTransactions();
        }

        public List<CurrentTransactions> GetCurrentTransactions()
        {
            return _reportRepository.GetCurrentTransactions();
        }
    }

    public interface IReportService
    {
        List<ByPopularity> GetByPopularities();
        List<ClientsPerformance> GetClientsPerformance();
        List<CurrentTransactions> GetCurrentTransactions();
        List<OverdueTransactions> GetOverdueTransactions();
    }
}
