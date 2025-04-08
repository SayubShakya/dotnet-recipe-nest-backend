// RoleService.cs

using RecipeNest.Model;
using RecipeNest.Reponse;
using RecipeNest.Repository;
using RecipeNest.Request;

namespace RecipeNest.Service;

public class RoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public List<RoleResponse> GetAll()
    {
        List<Role> roles = _roleRepository.GetAll();
        List<RoleResponse> roleResponses = [];
        foreach (var role in roles) roleResponses.Add(new RoleResponse(role.Id, role.Name));

        return roleResponses;
    }

    public RoleResponse GetById(int id)
    {
        var role = _roleRepository.GetById(id);

        return new RoleResponse(role.Id, role.Name);
    }


    public bool Save(CreateRoleRequest request)
    {
        var role = new Role();
        role.Name = request.Name;
        return _roleRepository.Save(role);
    }


    public bool Update(UpdateRoleRequest request)
    {
        var role = new Role();
        role.Id = request.Id;
        role.Name = request.Name;
        return _roleRepository.Update(role);
    }

    public bool DeleteById(int id)
    {
        return _roleRepository.DeleteById(id);
    }
}