using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemModels.Procedure
{
    [Keyless]
    public class ClientsPerformance
    {
        public string PersonName { get; set; }
        public int BooksTaken { get; set; }
    }
}
