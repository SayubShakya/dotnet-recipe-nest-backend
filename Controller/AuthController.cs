using RecipeNest.Dto;
using RecipeNest.Entity;
using RecipeNest.Repository;
using RecipeNest.Request;
using RecipeNest.Response;
using RecipeNest.Util;
using RecipeNest.Util.Impl;

namespace RecipeNest.Controller;

public class AuthController : BaseController
{
    private readonly IUserRepository _userRepository;
    private readonly IHashingUtil _hashingUtil;
    private readonly SessionUserDTO _sessionUserDto;

    public AuthController(IUserRepository userRepository, IHashingUtil hashingUtil, SessionUserDTO sessionUserDto)
    {
        _userRepository = userRepository;
        _hashingUtil = hashingUtil;
        _sessionUserDto = sessionUserDto;
    }

    public ServerResponse Login(LoginRequest request)
    {
        User? user = _userRepository.GetByEmail(request.Email);

        if (user == null)
        {
            return new ServerResponse(null, "Email/Password is incorrect", 401);
        }

        if (!user.IsActive)
        {
            return new ServerResponse(null, "Your account is disabled", 401);
        }

        if (!_hashingUtil.Verify(user.Password, request.Password))
        {
            return new ServerResponse(null, "Email/Password is incorrect", 401);
        }

        return new ServerResponse(TokenUtil.GenerateToken(user.Email), "Login Successful", 200);
    }

    public ServerResponse Register(RegisterRequest request)
    {
        var existingUser = _userRepository.GetByEmail(request.Email);
        if (existingUser != null)
        {
            return new ServerResponse(null, "An account with this email already exists.", 409);
        }

        try
        {
            string hashedPassword = _hashingUtil.Hash(request.Password);

            User user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Password = hashedPassword,
                RoleId = request.RoleId
            };

            bool saved = _userRepository.Save(user);

            if (saved)
            {
                return new ServerResponse(null, "Registration successful. Please log in.", 201);
            }

            Console.WriteLine($"Registration failed for email {request.Email} during the final save operation.");
            return new ServerResponse(null, "Registration failed due to an internal server error.", 500);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during registration for email {request.Email}: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            return new ServerResponse(null, "An unexpected error occurred during registration.", 500);
        }
    }


    public ServerResponse Authorized()
    {
        AuthorizedUserResponse authorizedUserResponse = new AuthorizedUserResponse(_sessionUserDto?.User?.Id,
            _sessionUserDto?.User?.FirstName + " " + _sessionUserDto?.User?.LastName, _sessionUserDto?.Role?.Name
        );
        return new ServerResponse(authorizedUserResponse, null, 200);
    }
}