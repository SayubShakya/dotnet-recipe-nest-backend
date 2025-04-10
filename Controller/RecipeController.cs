// Controller/RecipeController.cs

using RecipeNest.Request;
using RecipeNest.Response;
using RecipeNest.Service;

namespace RecipeNest.Controller;

public class RecipeController : BaseController
{
    private readonly RecipeService _recipeService;

    public RecipeController(RecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    public string GetAll(int start, int limit)
    {
        try
        {
            PaginatedResponse<RecipeResponse> response = _recipeService.GetAll(start, limit);
            ServerResponse serverResponse = new ServerResponse(response, null, 200);
            return ToJsonResponse(serverResponse);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in RecipeController.GetAll: {ex}");
            return ToJsonResponse(new ServerResponse(null, "Failed to retrieve recipe.", 500, ex.Message));
        }
    }

    public string GetById(int id)
    {
        var serverResponse = new ServerResponse(_recipeService.GetById(id), "Recipe found!", 200);
        return ToJsonResponse(serverResponse);
    }

    public string GetByTitle(string title)
    {
        var serverResponse =
            new ServerResponse(_recipeService.GetByTitle(title), "Recipe found by title!", 200);
        return ToJsonResponse(serverResponse);
    }

    public string Save(CreateRecipeRequest request)
    {
        var success = _recipeService.Save(request);
        if (success) return ToJsonResponse(new ServerResponse(null, "Recipe has been created!", 201));

        return ToJsonResponse(new ServerResponse(null, "Recipe creation failed!", 400));
    }

    public string Update(UpdateRecipeRequest request)
    {
        var success = _recipeService.Update(request);
        if (success) return ToJsonResponse(new ServerResponse(null, "Recipe has been updated!", 200));

        return ToJsonResponse(new ServerResponse(null, "Recipe update failed!", 400));
    }

    public string DeleteById(int id)
    {
        var serverRespose = new ServerResponse(_recipeService.DeleteById(id), "Recipe deleted!", 200);
        return ToJsonResponse(serverRespose);
    }
}