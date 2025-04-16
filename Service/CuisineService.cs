using RecipeNest.CustomException;
using RecipeNest.Dto;
using RecipeNest.Entity;
using RecipeNest.Repository;
using RecipeNest.Request;
using RecipeNest.Response;

namespace RecipeNest.Service;

public class CuisineService
{
    private readonly ICuisineRepository _cuisineRepository;

    public CuisineService(ICuisineRepository cuisineRepository)
    {
        _cuisineRepository = cuisineRepository;
    }

    public PaginatedResponse<CuisineResponse> GetAll(int start, int limit)
    {
        Paged<Cuisine> pagedCuisines = _cuisineRepository.GetAllPaginated(start, limit);
        
        List<CuisineResponse> items =  pagedCuisines.Items.Select(cuisine => new CuisineResponse(
            cuisine.Id,
            cuisine.Name,
            cuisine.ImageUrl
        )).ToList();

        PaginatedResponse<CuisineResponse> paginatedResponse = new()
        {
            Items = items,
            Count = pagedCuisines.Count,
            Limit = pagedCuisines.Limit,
            Start = pagedCuisines.Start
        };

        return paginatedResponse;
    }

    public CuisineResponse? GetById(int id)
    {
        var cuisine = _cuisineRepository.GetById(id);
        if (cuisine == null)  throw new CustomApplicationException(404, "Cuisine not found", null);

        return new CuisineResponse(
            cuisine.Id,
            cuisine.Name,
            cuisine.ImageUrl
        );
    }

    public CuisineResponse? GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;

        var cuisine = _cuisineRepository.GetByName(name);
        if (cuisine == null)  throw new CustomApplicationException(404, "Cuisine not found", null);

        return new CuisineResponse(
            cuisine.Id,
            cuisine.Name,
            cuisine.ImageUrl
        );
    }


    public bool Save(CreateCuisineRequest request)
    {
        var cuisine = new Cuisine
        {
            Name = request.Name,
            ImageUrl = request.ImageUrl
        };

        return _cuisineRepository.Save(cuisine);
    }

    public bool Update(UpdateCuisineRequest request)
    {
        var existingCuisine = _cuisineRepository.GetById(request.Id);
        if (existingCuisine == null)  throw new CustomApplicationException(404, "Cuisine not found", null);


        var cuisineToUpdate = new Cuisine
        {
            Id = request.Id,
            Name = request.Name,
            ImageUrl = request.ImageUrl
        };


        return _cuisineRepository.Update(cuisineToUpdate);
    }

    public bool DeleteById(int id)
    {
        var existingCuisine = _cuisineRepository.GetById(id);
        if (existingCuisine == null)  throw new CustomApplicationException(404, "Cuisine not found", null);
        return _cuisineRepository.DeleteById(id);
    }
}