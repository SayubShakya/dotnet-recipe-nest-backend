using RecipeNest.Request;
using RecipeNest.Response;
using RecipeNest.Service;
using System;
using RecipeNest.CustomException;

namespace RecipeNest.Controller;

public class CuisineController : BaseController
{
    private readonly CuisineService _cuisineService;

    public CuisineController(CuisineService cuisineService)
    {
        _cuisineService = cuisineService;
    }

    public ServerResponse GetAll(int start, int limit)
    {
        try
        {
            PaginatedResponse<CuisineResponse> response = _cuisineService.GetAll(start, limit);
            ServerResponse serverResponse = new ServerResponse(response, null, 200);
            return serverResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in CuisineController.GetAll: {ex}");
            return new ServerResponse(null, "Failed to retrieve cuisines.", 500, ex.Message);
        }
    }

    public ServerResponse GetById(int id)
    {
        try
        {
            var cuisineResponse = _cuisineService.GetById(id);
            if (cuisineResponse != null) return new ServerResponse(cuisineResponse, "Cuisine found!", 200);
            return new ServerResponse(null, "Cuisine not found", 404);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in CuisineController.GetById({id}): {ex}");
            return new ServerResponse(null, "Failed to retrieve cuisine.", 500, ex.Message);
        }
    }

    public ServerResponse GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new ServerResponse(null, "Cuisine name cannot be empty.", 400);
        try
        {
            var cuisineResponse = _cuisineService.GetByName(name);
            if (cuisineResponse != null)
                return new ServerResponse(cuisineResponse, "Cuisine found!", 200);

            return new ServerResponse(null, "Cuisine not found", 404);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in CuisineController.GetByName('{name}'): {ex}");
            return new ServerResponse(null, "Failed to retrieve cuisine by name.", 500, ex.Message);
        }
    }

    public ServerResponse Save(CreateCuisineRequest request)
    {
        if (request == null) return new ServerResponse(null, "Invalid request body.", 400);
        try
        {
            var success = _cuisineService.Save(request);
            if (success) return new ServerResponse(null, "Cuisine has been created!", 201);

            return new ServerResponse(null, "Cuisine creation failed.", 400);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in CuisineController.Save: {ex}");
            throw new CustomApplicationException(500, "Failed to save cuisine!", null);
        }
    }

    public ServerResponse Update(UpdateCuisineRequest request)
    {
        if (request == null) return new ServerResponse(null, "Invalid request body.", 400);
        try
        {
            var success = _cuisineService.Update(request);
            if (success) return new ServerResponse(null, "Cuisine has been updated!", 200);

            return new ServerResponse(null, "Cuisine update failed. Cuisine not found or invalid data.", 404);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in CuisineController.Update: {ex}");
            return new ServerResponse(null, "Cuisine update failed due to an internal error.", 500, ex.Message);
        }
    }

    public ServerResponse DeleteById(int id)
    {
        try
        {
            var success = _cuisineService.DeleteById(id);
            if (success) return new ServerResponse(null, "Cuisine has been deleted!", 200);
            return new ServerResponse(null, "Cuisine deletion failed. Cuisine not found.", 404);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in CuisineController.DeleteById({id}): {ex}");
            return new ServerResponse(null, "Cuisine deletion failed due to an internal error.", 500, ex.Message);
        }
    }
}