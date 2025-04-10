// UserController.cs

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

    public AuthController(IUserRepository userRepository, IHashingUtil hashingUtil)
    {
        _userRepository = userRepository;
        _hashingUtil = hashingUtil;
    }

    public ServerResponse Login(LoginRequest request)
    {
        try
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
        catch (Exception ex)
        {
            throw;
        }
    }
    
}