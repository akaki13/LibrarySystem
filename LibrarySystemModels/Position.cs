using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class Position
    {
        public Position()
        {
            PersonPositions = new HashSet<PersonPosition>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Salary { get; set; }
        public int LogsId { get; set; }

        public virtual TableLog Logs { get; set; }
        public virtual ICollection<PersonPosition> PersonPositions { get; set; }
    }
}
