// UserService.cs

using RecipeNest.Dto;
using RecipeNest.Model;
using RecipeNest.Repository;
using RecipeNest.Request;
using RecipeNest.Response;
using RecipeNest.Util;

namespace RecipeNest.Service;

public class UserService
{
    private readonly IHashingUtil _hashingUtil;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, IHashingUtil hashingUtil)
    {
        _userRepository = userRepository;
        _hashingUtil = hashingUtil;
    }
    
    public PaginatedResponse<UserResponse> GetAll(int start, int limit)
    {
        Paged<User> pagedUsers = _userRepository.GetAllPaginated(start, limit);
        
        List<UserResponse> items =  pagedUsers.Items.Select(user => new UserResponse(
                user.Id,
                user.FirstName,
                user.LastName,
                user.PhoneNumber,
                user.ImageUrl,
                user.About,
                user.Email,
                user.RoleId,
                user.IsActive
        )).ToList();

        PaginatedResponse<UserResponse> paginatedResponse = new()
        {
            Items = items,
            Count = pagedUsers.Count,
            Limit = pagedUsers.Limit,
            Start = pagedUsers.Start
        };

        return paginatedResponse;
    }

    public UserResponse GetById(int id)
    {
        var user = _userRepository.GetById(id);

        return new UserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.ImageUrl,
            user.About,
            user.Email,
            user.RoleId,
            user.IsActive
        );
    }

    public bool Save(CreateUserRequest request)
    {
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            ImageUrl = request.ImageUrl,
            About = request.About,
            Email = request.Email,
            RoleId = request.RoleId,
            Password = _hashingUtil.Hash(request.Password)
        };

        return _userRepository.Save(user);
    }

    public bool Update(UpdateUserRequest request)
    {
        var existingUser = _userRepository.GetById(request.Id) ?? throw new ArgumentNullException(nameof(request));

        var user = new User
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            ImageUrl = request.ImageUrl,
            About = request.About,
            Email = request.Email,
            RoleId = request.RoleId,
            Password = !string.IsNullOrEmpty(request.Password) ? request.Password : existingUser.Password
        };


        return _userRepository.Update(user);
    }

    public bool DeleteById(int id)
    {
        return _userRepository.DeleteById(id);
    }


    public UserResponse? GetByEmail(string email)
    {
        var user = _userRepository.GetByEmail(email);
        if (user == null) return null;

        return new UserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.ImageUrl,
            user.About,
            user.Email,
            user.RoleId,
            user.IsActive
        );
    }
}