using LibrarySystemData.Infrastructure;
using LibrarySystemModels;
using LibrarySystemModels.Parameters;
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

        public List<ByPopularity> GetByPopularities(ByPopularityParameters parameters)
        {
            return _context.Set<ByPopularity>()
              .FromSqlRaw("EXEC GetByPopularity @BookNameSearch = {0}, @MinTimesTaken = {1}, @MaxTimesTaken = {2};",
               parameters.BookName, parameters.MinBooksTaken, parameters.MaxBooksTaken)
               .ToList();
        }
        public List<ClientsPerformance> GetClientsPerformance(ClientsPerformanceParameters parameters)
        {
            var con = _context.Set<ClientsPerformance>()
                .FromSqlRaw("EXEC ClientsPerformance @PersonNameSearch = {0}, @MinBooksTaken = {1}, @MaxBooksTaken = {2};",
               parameters.PersonNameSearch, parameters.MinBooksTaken, parameters.MaxBooksTaken)
               .ToList();
            return con;
        }

        public List<OverdueTransactions> GetOverdueTransactions()
        {
            var con =  _context.Set<OverdueTransactions>().FromSqlRaw("EXEC OverdueTransactions").ToList();
            return con;
        }

        public List<CurrentTransactions> GetCurrentTransactions(CurrentTransactionsParameters parameters )
        {
            return _context.Set<CurrentTransactions>()
                .FromSqlRaw("EXEC CurrentTransactions @PersonNameSearch = {0},@BookNameSearch = {1}, @TakeTimeAfter = {2}," +
                "@TakeTimeBefore = {3},@ReturnTimeAfter = {4},@ReturnTimeBefore = {5};",
                parameters.PersonNameSearch, parameters.BookNameSearch, parameters.TakeTimeAfter, parameters.TakeTimeBefore,
                parameters.ReturnTimeAfter, parameters.ReturnTimeBefore).ToList();
        }

    }

    public interface IReportRepository 
    {
        List<ByPopularity> GetByPopularities(ByPopularityParameters parameters);
        List<ClientsPerformance> GetClientsPerformance(ClientsPerformanceParameters parameters);
        List<CurrentTransactions> GetCurrentTransactions(CurrentTransactionsParameters parameters);
        List<OverdueTransactions> GetOverdueTransactions();
    }
}
