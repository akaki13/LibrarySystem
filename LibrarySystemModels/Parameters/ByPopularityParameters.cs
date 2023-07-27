using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemModels.Parameters
{

    public class ByPopularityParameters
    {
        public string BookName { get; set; }
        public int? MinBooksTaken { get; set; }
        public int? MaxBooksTaken { get; set; }
    }
}
