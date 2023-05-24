using LibrarySystemData.Repositories;
using LibrarySystemModels;

namespace LibraryService
{
    public class RoleUserService : IRoleUserService
    {
        private readonly IRoleUserRepository _roleUserRepository;

        public RoleUserService(IRoleUserRepository roleUserRepository) 
        {
            _roleUserRepository = roleUserRepository;
        }

        public void Add(RoleUser user)
        {
            _roleUserRepository.AddData(user);
        }

        public void Save()
        {
            _roleUserRepository.SaveData();
        }

        public RoleUser GetByUserId(int id)
        {
            return _roleUserRepository.FindByUserId(id).Result;
        }
        public List<RoleUser> GetAll()
        {
            return _roleUserRepository.TakeAll().Result;
        }
    }

    public interface IRoleUserService
    {
        void Add(RoleUser user);
        void Save();
        RoleUser GetByUserId(int id);
        List<RoleUser> GetAll();
    }
}
