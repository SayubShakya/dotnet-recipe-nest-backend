using RecipeNest.CustomException;
using RecipeNest.Dto;
using RecipeNest.Entity;
using RecipeNest.Repository;
using RecipeNest.Request;
using RecipeNest.Response;

namespace RecipeNest.Service;

public class RoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }


    public PaginatedResponse<RoleResponse> GetAll(int start, int limit)
    {
        Paged<Role> pagedRoles = _roleRepository.GetAllPaginated(start, limit);

        List<RoleResponse> items = pagedRoles.Items.Select(role => new RoleResponse(
            role.Id,
            role.Name
        )).ToList();

        PaginatedResponse<RoleResponse> paginatedResponse = new()
        {
            Items = items,
            Count = pagedRoles.Count,
            Limit = pagedRoles.Limit,
            Start = pagedRoles.Start
        };

        return paginatedResponse;
    }

    public RoleResponse GetById(int id)
    {
        var role = _roleRepository.GetActiveById(id);
        if (role == null) throw new CustomApplicationException(404, "Roles not found", null);
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
        var existingRole = _roleRepository.GetActiveById(request.Id);
        if (existingRole == null) throw new CustomApplicationException(404, "Roles not found", null);

        var role = new Role();
        role.Id = request.Id;
        role.Name = request.Name;
        return _roleRepository.Update(role);
    }


    public bool DeleteById(int id)
    {
        var existingRole = _roleRepository.GetActiveById(id);
        if (existingRole == null) throw new CustomApplicationException(404, "Roles not found", null);
        return _roleRepository.DeleteById(id);
    }
}