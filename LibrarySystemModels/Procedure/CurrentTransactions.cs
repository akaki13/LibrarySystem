﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemModels.Procedure
{
    [Keyless]
    public class CurrentTransactions
    {
        public string PersonName { get; set; }
        public string BookName { get; set; }
        public DateTime ReturTime { get; set; }
        public DateTime TakeTime { get; set; }
    }
}
