// RoleService.cs

using System.Collections.Generic;
using RecipeNest.Model;
using RecipeNest.Reponse;
using RecipeNest.Repository;
using RecipeNest.Request;

namespace RecipeNest.Service
{
    public class RoleService
    {
        private IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        public List<RoleResponse> GetAll()
        {
            List<Role> roles = _roleRepository.GetAll();
            List<RoleResponse> roleResponses = [];
            foreach (Role role in roles)
            {
                roleResponses.Add(new RoleResponse(role.Id, role.Name));
            }

            return roleResponses;
        }

        public RoleResponse GetById(int id)
        {
            Role role = _roleRepository.GetById(id);

            return new RoleResponse(role.Id, role.Name);
        }


        public bool Save(CreateRoleRequest request)
        {
            Role role = new Role();
            role.Name = request.Name;
            return _roleRepository.Save(role);
        }


        public bool Update(UpdateRoleRequest request)
        {
            Role role = new Role();
            role.Id = request.Id;
            role.Name = request.Name;
            return _roleRepository.Update(role);
        }

        public bool DeleteById(int id)
        {
            return _roleRepository.DeleteById(id);
        }
    }
}