// Controller/RecipeController.cs

using RecipeNest.Model;
using RecipeNest.Reponse;
using RecipeNest.Request;
using RecipeNest.Service;

namespace RecipeNest.Controller
{
    public class RecipeController : BaseController
    {
        private RecipeService recipeService;

        public RecipeController(RecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        public string GetAll()
        {
            ServerResponse serverResponse = new ServerResponse(recipeService.GetAll(), "Recipe list!", 200);
            return ToJsonResponse(serverResponse);
        }

        public string GetById(int id)
        {
            ServerResponse serverResponse = new ServerResponse(recipeService.GetById(id), "Recipe found!", 200);
            return ToJsonResponse(serverResponse);
        }

        public string GetByTitle(string title)
        {
            ServerResponse serverResponse =
                new ServerResponse(recipeService.GetByTitle(title), "Recipe found by title!", 200);
            return ToJsonResponse(serverResponse);
        }

        public string Save(CreateRecipeRequest request)
        {
            bool success = recipeService.Save(request);
            if (success)
            {
                return ToJsonResponse(new ServerResponse(null, "Recipe has been created!", 201));
            }
            else
            {
                return ToJsonResponse(new ServerResponse(null, "Recipe creation failed!", 400));
            }
        }

        public string Update(UpdateRecipeRequest request)
        {
            bool success = recipeService.Update(request);
            if (success)
            {
                return ToJsonResponse(new ServerResponse(null, "Recipe has been updated!", 200));
            }
            else
            {
                return ToJsonResponse(new ServerResponse(null, "Recipe update failed!", 400));
            }
        }

        public string DeleteById(int id)
        {
            ServerResponse serverRespose = new ServerResponse(recipeService.DeleteById(id), "Recipe deleted!", 200);
            return ToJsonResponse(serverRespose);
        }
    }
}