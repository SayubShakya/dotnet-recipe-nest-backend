using RecipeNest.CustomException;
using RecipeNest.Dto;
using RecipeNest.Entity;
using RecipeNest.Projection;
using RecipeNest.Repository;
using RecipeNest.Request;
using RecipeNest.Response;
using RecipeNest.Util;

namespace RecipeNest.Service;

public class UserService
{
    private readonly IHashingUtil _hashingUtil;
    private readonly SessionUser _sessionUser;
    private readonly IUserRepository _userRepository;

    public UserService(IHashingUtil hashingUtil, SessionUser sessionUser, IUserRepository userRepository)
    {
        _hashingUtil = hashingUtil;
        _sessionUser = sessionUser;
        _userRepository = userRepository;
    }


    public PaginatedResponse<UserTableResponse> GetAll(int start, int limit)
    {
        Paged<UserTableProjection> users = _userRepository.GetAllPaginated(start, limit);

        List<UserTableResponse> items = users.Items.Select(MapUserTableResponse).ToList();

        PaginatedResponse<UserTableResponse> paginatedResponse = new()
        {
            Items = items,
            Count = users.Count,
            Limit = users.Limit,
            Start = users.Start
        };

        return paginatedResponse;
    }
    
    public PaginatedResponse<ChefTableResponse> GetAllActiveChef(int start, int limit)
    {
        Paged<ChefTableProjection> users = _userRepository.GetAllActiveChef(start, limit);

        List<ChefTableResponse> items = users.Items.Select(MapChefTableResponse).ToList();

        PaginatedResponse<ChefTableResponse> paginatedResponse = new()
        {
            Items = items,
            Count = users.Count,
            Limit = users.Limit,
            Start = users.Start
        };

        return paginatedResponse;
    }
    
    private static ChefTableResponse MapChefTableResponse(ChefTableProjection user)
    {
        return new ChefTableResponse()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email
        };
    }

    private static UserTableResponse MapUserTableResponse(UserTableProjection user)
    {
        return new UserTableResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            Role = user.Role,
            IsActive = user.IsActive
        };
    }

    public UserResponse GetActiveById(int id)
    {
        UserDetailProjection? userDetailProjection = _userRepository.GetUserDetailProjectionById(id);
        if (userDetailProjection == null) throw new CustomApplicationException(404, "Users not found", null);

        return new UserResponse
        {
            Id = userDetailProjection.Id,
            FirstName = userDetailProjection.FirstName,
            LastName = userDetailProjection.LastName,
            PhoneNumber = userDetailProjection.PhoneNumber,
            ImageUrl = userDetailProjection.ImageUrl,
            About = userDetailProjection.About,
            Email = userDetailProjection.Email,
            Role = userDetailProjection.Role,
            IsActive = userDetailProjection.IsActive,
        };
    }

    public void Save(CreateUserRequest request)
    {
        if (_userRepository.GetByEmail(request.Email) != null)
        {
            throw new CustomApplicationException(400, "Email already exists", null);
        }

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

        _userRepository.Save(user);
    }

    public bool Update(UpdateUserRequest request)
    {
        var existingUser = _userRepository.GetActiveById(request.Id);
        if (existingUser == null) throw new CustomApplicationException(404, "Users not found", null);

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
    
    public bool UpdateProfile(UpdateUserProfileRequest request)
    {
       int id =  _sessionUser.User.Id;

        var user = new User
        {
            Id = id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            ImageUrl = request.ImageUrl,
            About = request.About
        };

        return _userRepository.UpdateProfile(user);
    }

    public bool Deactivate(int id)
    {
        var user = FindById(id);
        if (!user.IsActive) throw new CustomApplicationException(409, "Users is already deactivated!", null);
        return _userRepository.DeleteById(id);
    }

    public bool Activate(int id)
    {
        var user = FindById(id);
        if (user.IsActive) throw new CustomApplicationException(409, "Users is already activated!", null);
        return _userRepository.RestoreById(id);
    }
    
    private User? FindById(int id)
    {
        User user = _userRepository.GetById(id);
        if (user == null) throw new CustomApplicationException(404, "Users not found", null);
        return user;
    }
}