using LibrarySystemData.Repositories;
using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetById(int? id)
        {
            return _userRepository.FindById(id).Result;
        }

        public User GetByUserName(string userName)
        {
            return _userRepository.FindByUserName(userName).Result;
        }

        public void Update(User user)
        {
            _userRepository.UpdateData(user);
        }

        public void Add(User user)
        {
            _userRepository.AddData(user);
        }

        public void Save()
        {
            _userRepository.SaveData();
        }
        public User GetByLoginInfo(string userName,string password)
        {
            return _userRepository.FindByLoginInfo(userName,password).Result;
        }

        public List<User> GetAllWithPerson()
        {
            return _userRepository.TakeAllWithPerson().Result;
        }

        public User GetByPersonId(int id)
        {
            return _userRepository.FindByPersonId(id).Result;
        }

        public List<User> GetAll()
        {
            return _userRepository.TakeAll().Result;
        }
    }

    public interface IUserService { 
        User GetById(int? id);
        User GetByUserName(string userName);
        void Update(User user);
        void Add(User user);
        void Save();
        User GetByLoginInfo(string userName, string password);
        User GetByPersonId(int id);
        List<User> GetAllWithPerson();
        List<User> GetAll();
    }
}
