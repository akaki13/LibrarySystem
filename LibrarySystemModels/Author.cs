﻿using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class Author
    {
        public Author()
        {
            AuthorBooks = new HashSet<AuthorBook>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
