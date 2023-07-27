using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemModels.Parameters
{

    public class OverdueTransactionsParameters
    {
        public string PersonNameSearch { get; set; }
        public string BookNameSearch { get; set; }
        public DateTime? ReturnTimeAfter { get; set; }
        public DateTime? ReturnTimeBefore { get; set; }
    }
}
