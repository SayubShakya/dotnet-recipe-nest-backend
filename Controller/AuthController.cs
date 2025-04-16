using RecipeNest.Dto;
using RecipeNest.Model;
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
        var user = _userRepository.GetByEmail(request.Email);
        if (user != null)
        {
            string hashedPassword = user.Password;
            if (_hashingUtil.Verify(hashedPassword, request.Password))
            {
                return new ServerResponse(TokenUtil.GenerateToken(user.Email), "Login Successful", 200);
            }
        }

        return new ServerResponse(null, "Email/Password is incorrect", 401);
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

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Password = hashedPassword,
                RoleId = request.RoleId,
                ImageUrl = null,
                About = null,
            };

            bool saved = _userRepository.Save(newUser);

            if (saved)
            {
                return new ServerResponse(null, "Registration successful. Please log in.", 201);
            }
            else
            {
                Console.WriteLine($"Registration failed for email {request.Email} during the final save operation.");
                return new ServerResponse(null, "Registration failed due to an internal server error.", 500);
            }
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
        AuthorizedUserResponse authorizedUserResponse = new AuthorizedUserResponse(
            _sessionUserDto?.User?.FirstName + " " + _sessionUserDto?.User?.LastName,
            _sessionUserDto?.Role?.Name
        );
        return new ServerResponse(authorizedUserResponse, null, 200);
    }
}