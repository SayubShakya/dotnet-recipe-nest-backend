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

    public ServerResponse GetAll(int start, int limit)
    {
        try
        {
            PaginatedResponse<RecipeResponse> response = _recipeService.GetAll(start, limit);
            return new ServerResponse(response, null, 200);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in RecipeController.GetAll: {ex}");
            return new ServerResponse(null, "Failed to retrieve recipe.", 500, ex.Message);
        }
    }

    public ServerResponse GetById(int id)
    {
        return new ServerResponse(_recipeService.GetById(id), "Recipe found!", 200);
    }

    public ServerResponse GetByTitle(string title)
    {
        return new ServerResponse(_recipeService.GetByTitle(title), "Recipe found by title!", 200);
    }

    public ServerResponse Save(CreateRecipeRequest request)
    {
        var success = _recipeService.Save(request);
        if (success) return new ServerResponse(null, "Recipe has been created!", 201);
        return new ServerResponse(null, "Recipe creation failed!", 400);
    }

    public ServerResponse Update(UpdateRecipeRequest request)
    {
        var success = _recipeService.Update(request);
        if (success) return new ServerResponse(null, "Recipe has been updated!", 200);
        return new ServerResponse(null, "Recipe update failed!", 400);
    }

    public ServerResponse DeleteById(int id)
    {
        return new ServerResponse(_recipeService.DeleteById(id), "Recipe deleted!", 200);
    }
}