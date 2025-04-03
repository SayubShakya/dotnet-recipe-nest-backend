// UserService.cs

using System.Security.Policy;
using RecipeNest.Model;     
using RecipeNest.Reponse;  
using RecipeNest.Repository; 
using RecipeNest.Request;
using RecipeNest.Util;

namespace RecipeNest.Service
{
    public class UserService
    {
        private readonly IUserRepository _userRepository; 
        private readonly IHashingUtil _hashingUtil;


        public UserService(IUserRepository userRepository, IHashingUtil hashingUtil)
        {
            _userRepository = userRepository;
            _hashingUtil = hashingUtil;
        }

        public List<UserResponse> GetAll() 
        {
            List<User> users = _userRepository.GetAll();
            List<UserResponse> userResponses = []; 
            foreach (User user in users)
            {
                userResponses.Add(new UserResponse(
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.PhoneNumber,
                    user.ImageUrl,
                    user.About,
                    user.Email,
                    user.RoleId
                ));
            }
            return userResponses;
        }

        public UserResponse GetById(int id) 
        {
            User user = _userRepository.GetById(id);

            return new UserResponse(
                user.Id,
                user.FirstName,
                user.LastName,
                user.PhoneNumber,
                user.ImageUrl,
                user.About,
                user.Email,
                user.RoleId
            );
        }

        public bool Save(CreateUserRequest request) 
        {
            User user = new User
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
            User existingUser = _userRepository.GetById(request.Id) ?? throw new ArgumentNullException(nameof(request));

            User user = new User
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
            User? user = _userRepository.GetByEmail(email);
            if (user == null)
            {
                return null; 
            }

            return new UserResponse(
                user.Id,
                user.FirstName,
                user.LastName,
                user.PhoneNumber,
                user.ImageUrl,
                user.About,
                user.Email,
                user.RoleId
            );
        }
    }
}