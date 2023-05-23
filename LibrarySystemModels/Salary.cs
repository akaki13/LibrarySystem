using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class Salary
    {
        public int Id { get; set; }
        public int? Amount { get; set; }
        public int? PersonId { get; set; }
        public int? LogsId { get; set; }

        public virtual TableLog Logs { get; set; }
        public virtual Person Person { get; set; }
    }
}
