using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class TableLog
    {

        public int Id { get; set; }
        public string TableName { get; set; }
        public int? TableId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
