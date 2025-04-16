// using RecipeNest.Consta;
// using RecipeNest.Db;
// using RecipeNest.Db.Query.Impl;
// using RecipeNest.Dto;
// using RecipeNest.Model;
// using RecipeNest.Projection;
// using RecipeNest.Response;
//
// namespace RecipeNest.Repository.Impl.Database;
//
// public class UserRoleRepositoryDatabaseImpl : IUserRoleRepository
// {
//     Paged<User> IUserRoleRepository.GetAllWithRolesPaginated(int start, int limit)
//     {
//         return DatabaseConnector.QueryAll(IQueryConstant.IUser.GetUsersWithRoles, IQueryConstant.IUser.AllActiveCount, start, limit, new UserRoleRowMapper());
//     }
//
//     public bool Save(UserRoleResponse obj)
//     {
//         throw new NotImplementedException();
//     }
//
//     public bool Update(UserRoleResponse obj)
//     {
//         throw new NotImplementedException();
//     }
//
//     public UserRoleResponse GetById(int id)
//     {
//         throw new NotImplementedException();
//     }
//
//     public List<UserRoleResponse> GetAll()
//     {
//         throw new NotImplementedException();
//     }
//
//     public bool DeleteById(int id)
//     {
//         throw new NotImplementedException();
//     }
//
//
// }