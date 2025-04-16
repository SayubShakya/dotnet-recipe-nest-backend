﻿using RecipeNest.Dto;
using RecipeNest.Model;
using RecipeNest.Projection;

namespace RecipeNest.Repository;

public interface IUserRepository : IBaseRepository<User>
{
    User? GetByEmail(string email);
    Paged<User> GetAllPaginated(int start, int limit);
}