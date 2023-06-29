using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class AuthorBook
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? AutorId { get; set; }

        public virtual Author Autor { get; set; }
        public virtual Book Book { get; set; }
    }
}
