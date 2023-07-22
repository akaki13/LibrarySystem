using LibrarySystemData.Infrastructure;
using LibrarySystemModels;
using LibrarySystemModels.Procedure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly LibraryContext _context;
        public ReportRepository(LibraryContext context)
        {
            _context = context;
        }

        public List<ByPopularity> GetByPopularities()
        {
            return _context.Set<ByPopularity>().FromSqlRaw("EXEC GetByPopularity").ToList();
            

        }
        public List<ClientsPerformance> GetClientsPerformance()
        {
            return _context.Set<ClientsPerformance>().FromSqlRaw("EXEC ClientsPerformance").ToList();
        }

        public List<OverdueTransactions> GetOverdueTransactions()
        {
            var con =  _context.Set<OverdueTransactions>().FromSqlRaw("EXEC OverdueTransactions").ToList();
            return con;
        }

        public List<CurrentTransactions> GetCurrentTransactions()
        {
            return _context.Set<CurrentTransactions>().FromSqlRaw("EXEC CurrentTransactions").ToList();
        }

    }

    public interface IReportRepository 
    {
        List<ByPopularity> GetByPopularities();
        List<ClientsPerformance> GetClientsPerformance();
        List<CurrentTransactions> GetCurrentTransactions();
        List<OverdueTransactions> GetOverdueTransactions();
    }
}
