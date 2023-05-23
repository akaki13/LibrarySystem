using LibrarySystemData.Repositories;
using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public Person GetById(int? id)
        {
            return _personRepository.FindById(id).Result;
        }
        public void Update(Person person)
        { 
            _personRepository.UpdateData(person);
        }
        public void Add(Person person)
        {
            _personRepository.AddData(person);
        }
        public void Save()
        {
            _personRepository.SaveData();
        }
        public Person GetByEmail(string email)
        {
            return _personRepository.FindByEmail(email).Result; 
        }
        public Person GetByPhone(string phone)
        {
            return _personRepository.FindByPhone(phone).Result;
        }
    }
    public interface IPersonService
    {
        Person GetById(int? id);
        void Update(Person person);
        void Add(Person person);
        void Save();
        Person GetByEmail(string email);
        Person GetByPhone(string phone);

    }
}
