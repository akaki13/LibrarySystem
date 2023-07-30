using LibrarySystemData.Repositories;
using LibrarySystemModels;
using LibrarySystemModels.Parameters;
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

        public List<ByPopularity> GetByPopularities(ByPopularityParameters parameters)
        {
            return _reportRepository.GetByPopularities( parameters);

        }

        public List<ClientsPerformance> GetClientsPerformance(ClientsPerformanceParameters parameters)
        {
            return _reportRepository.GetClientsPerformance( parameters);
        }

        public List<OverdueTransactions> GetOverdueTransactions(OverdueTransactionsParameters parameters)
        {
            return _reportRepository.GetOverdueTransactions( parameters);
        }

        public List<CurrentTransactions> GetCurrentTransactions(CurrentTransactionsParameters parameters)
        {
            return _reportRepository.GetCurrentTransactions( parameters);
        }
    }

    public interface IReportService
    {
        List<ByPopularity> GetByPopularities(ByPopularityParameters parameters);
        List<ClientsPerformance> GetClientsPerformance(ClientsPerformanceParameters parameters);
        List<CurrentTransactions> GetCurrentTransactions(CurrentTransactionsParameters parameters);
        List<OverdueTransactions> GetOverdueTransactions(OverdueTransactionsParameters parameters);
    }
}
