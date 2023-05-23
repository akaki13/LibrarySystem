using LibrarySystemData.Repositories;
using LibrarySystemModels;

namespace LibraryService
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository) 
        {
            _roleRepository = roleRepository;
        }

        public Role GetByTitle(string title)
        {
            return _roleRepository.TakeByTitle(title).Result;
        }

        public List<Role> GetRoles()
        {
            return _roleRepository.TakeAll().Result;
        }

    }

    public interface IRoleService
    {
        Role GetByTitle(string title);
        List<Role> GetRoles();
    }
}
