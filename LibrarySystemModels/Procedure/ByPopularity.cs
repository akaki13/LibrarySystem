using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemModels.Procedure
{
    [Keyless]
    public class ByPopularity
    {
        public string BookName { get; set; }
        public int TimesTaken { get; set; }
    }
}
