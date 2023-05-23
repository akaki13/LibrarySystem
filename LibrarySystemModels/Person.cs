using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class Person
    {
        public Person()
        {
            Borrows = new HashSet<Borrow>();
            PersonPositions = new HashSet<PersonPosition>();
            Salaries = new HashSet<Salary>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool? EmailIsConfiormed { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int LogsId { get; set; }

        public virtual TableLog Logs { get; set; }
        public virtual ICollection<Borrow> Borrows { get; set; }
        public virtual ICollection<PersonPosition> PersonPositions { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
