using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class Borrow
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? PersonId { get; set; }
        public DateTime? TakeTime { get; set; }
        public DateTime? ReturnedTime { get; set; }
        public DateTime? ActualReturnedTime { get; set; }
        public int LogsId { get; set; }

        public virtual Book Book { get; set; }
        public virtual TableLog Logs { get; set; }
        public virtual Person Person { get; set; }
    }
}
