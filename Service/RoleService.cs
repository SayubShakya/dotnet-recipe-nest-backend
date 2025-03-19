using Model;
using Repository;

namespace RecipeNest.Service
{

    public class RoleService
    {
        private RoleRepository roleRepository;

        public RoleService(RoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public List<Role> GetAll()
        {
            return roleRepository.GetAll();
        }

    }
}