//QueryConstant.cs

namespace RecipeNest.Constant;

public interface IQueryConstant
{
    public interface IRole
    {
        const string Save = "INSERT INTO roles(name) VALUES (@param1)";
        public const string Update = "UPDATE roles SET name=@param1 WHERE is_active=1 and id=@param2";
        public const string GetById = "SELECT * FROM roles WHERE id=@param1 and is_active=1";

        public const string GetAllActiveOrderByCreatedDate =
            "SELECT * FROM roles WHERE is_active=1 ORDER BY created_date";

        public const string AllActiveCount =
            "SELECT count(*) as count FROM roles WHERE is_active=1";

        public const string DeleteById = "UPDATE roles set is_active=0 WHERE id=@param1 And is_active = 1";
    }

    public interface IUser
    {
        public const string Save =
            " INSERT INTO users (first_name, last_name, phone_number, image_url, about, email, password, role_id) VALUES ( @param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8)";

        public const string Update =
            "UPDATE users SET first_name=@param1, last_name=@param2, phone_number=@param3,image_url=@param4, about=@param5, email=@param6, password=@param7, role_id=@param8 WHERE is_active=1 AND id=@param9";
        
        public const string UpdateProfile =
            "UPDATE users SET first_name=@param1, last_name=@param2, phone_number=@param3,image_url=@param4, about=@param5 WHERE is_active=1 AND id=@param6";

        public const string GetActiveById = "SELECT * FROM users WHERE id=@param1 AND is_active=1";
        public const string GetInactiveById = "SELECT * FROM users WHERE id=@param1 AND is_active=0";

        public const string GetAllActiveOrderByCreatedDate =
            "SELECT * FROM users WHERE is_active=1 ORDER BY created_date";

        public const string GetUsersWithRoles =
            "SELECT u.id, u.first_name, u.last_name, u.phone_number, u.email, r.name as role, u.is_active FROM users u LEFT JOIN roles r ON u.role_id = r.id ORDER BY u.created_date";

        public const string GetUsersWithRolesCount =
            "SELECT count(*) as count FROM users u LEFT JOIN roles r ON u.role_id = r.id ORDER BY u.created_date";

        public const string GetUserDetailByIdProjection =
            "SELECT u.id, u.first_name, u.last_name, u.phone_number, u.email, r.name as role, u.is_active, u.about, u.image_url FROM users u LEFT JOIN roles r ON u.role_id = r.id WHERE u.id=@param1 AND u.is_active=1 ORDER BY u.created_date";

        public const string DeleteById = "UPDATE users SET is_active=0 WHERE id=@param1 AND is_active = 1";
        public const string RestoreById = "UPDATE users SET is_active=1 WHERE id=@param1 AND is_active = 0";
        public const string GetById= "SELECT * FROM users WHERE id=@param1";
        public const string GetByEmail = "SELECT * FROM users WHERE email=@param1";
    }

    public interface ICuisine
    {
        public const string Save = "INSERT INTO cuisines (name, image_url) VALUES (@param1, @param2) ";

        public const string Update =
            "UPDATE cuisines SET name=@param1, image_url=@param2 WHERE is_active=1 AND id=@param3";

        public const string GetById = "SELECT * FROM cuisines WHERE id=@param1 AND is_active=1";

        public const string GetAllActiveOrderByCreatedDate =
            "SELECT * FROM cuisines WHERE is_active=1 ORDER BY created_date";

        public const string AllActiveCount =
            "SELECT count(*) as count FROM cuisines WHERE is_active=1";

        public const string DeleteById = "UPDATE cuisines SET is_active=0 WHERE id=@param1 AND is_active = 1";
        public const string GetByName = "SELECT * FROM cuisines WHERE name=@param1 AND is_active=1";
    }

    public interface IRecipe
    {
        public const string Save =
            "INSERT INTO recipes (image_url, title, description, recipe, ingredients, recipe_by, cuisine) VALUES (@param1, @param2, @param3, @param4, @param5, @param6, @param7)";

        public const string Update =
            "UPDATE recipes SET image_url=@param1, title=@param2, description=@param3, recipe=@param4, ingredients=@param5, recipe_by=@param6, cuisine=@param7 WHERE is_active=1 AND id=@param8";

        public const string GetById = "SELECT * FROM recipes WHERE id=@param1 AND is_active=1";

        public const string GetAllFavorites =
            "SELECT r.*, ra.rating\nFROM (SELECT r.*\n      FROM favorites f\n               LEFT JOIN recipes r on r.id = f.recipe_id\n      where f.user_id = 25\n        and f.is_active = 1) r\n         LEFT JOIN (SELECT AVG(rating) as rating, recipe_id FROM ratings WHERE is_active = 1 GROUP BY recipe_id ) ra ON r.id = ra.recipe_id\n         LEFT JOIN (SELECT * FROM favorites WHERE is_active = 1 AND user_id = 25) fa ON r.id = fa.recipe_id\nWHERE r.is_active = 1\nORDER BY r.created_date\nLIMIT 30 OFFSET 0;\n  var favorite = new Favorite\n        {\n            UserId = userId,\n            RecipeId = request.RecipeId\n        };\n\n        var success = _favoriteRepository.Save(favorite);\nid\n    try\n        {\n            return DatabaseConnector.Update(IQueryConstant.IFavorite.DeleteById, favoriteId) > 0;\n        }\n        catch (Exception ex)\n        {\n            Console.WriteLine($\"Error deleting favorite by user/recipe: {ex.Message}\");\n            return false;\n        }\nbody, div, td, p {font-family:'Inter'; font-size:13pt; color:#dfe1e5;} a {font-family:'Inter'; font-size:13pt; color:#6b9bfa;} ul {list-style:disc; margin-left:15px;} \nOverriding methods of 'Favorite' were not found\nfavoriteId\nuserId\n_sessionUser.User.Id\n_favoriteRepository\nIFavoriteRepository\nsessionUser\n        \n        if (Regex.IsMatch(path, @\"^/favorites/?(?:\\?.*)?\"))\n        {\n            var userId = Convert.ToInt32(request.QueryString[\"user_id\"]);\n            var recipeId = Convert.ToInt32(request.QueryString[\"recipe_id\"]);\n\n            if (request.HttpMethod.Equals(\"GET\")) return _favoriteController.GetByUserAndRecipe(userId, recipeId);\n        }\n        \n@\"^/cuisines/?(?:\\?.*)?\n  else if (Regex.IsMatch(path, @\"^/favorites/?$\"))\n        {\n            if (request.HttpMethod.Equals(\"POST\"))\n                return _favoriteController.Save(BaseController.JsonRequestBody<CreateFavoriteRequest>(request));\n            \n            if (request.HttpMethod.Equals(\"POST\"))\n                return _favoriteController.Save(BaseController.JsonRequestBody<CreateFavoriteRequest>(request));\n        }\n  if (request.HttpMethod.Equals(\"POST\"))\n                return _favoriteController.Save(BaseController.JsonRequestBody<CreateFavoriteRequest>(request));\nIsFavorite\n\n            IsFavorite = reader.GetBoolean(\"is_favorite\"),\nreader.GetInt32(\"rating\")\nrating\nreader\nRating =  reader.GetInt32(\"rating\"),\nSELECT r.*, ra.rating, fa.is_active as is_favorite\nFROM recipes r\n         LEFT JOIN (SELECT * FROM ratings WHERE is_active=1 AND user_id=@param1) ra ON r.id = ra.recipe_id\n         LEFT JOIN (SELECT * FROM favorites WHERE is_active=1 AND user_id=@param2) fa ON r.id = fa.recipe_id\nWHERE r.is_active = 1\nORDER BY r.created_date";

        public const string CountAllFavorites =
            "SELECT count(*) as count\nFROM (SELECT r.*\n      FROM favorites f\n               LEFT JOIN recipes r on r.id = f.recipe_id\n      where f.user_id = 25\n        and f.is_active = 1) r\n         LEFT JOIN (SELECT AVG(rating) as rating, recipe_id FROM ratings WHERE is_active = 1 GROUP BY recipe_id ) ra ON r.id = ra.recipe_id\n         LEFT JOIN (SELECT * FROM favorites WHERE is_active = 1 AND user_id = 25) fa ON r.id = fa.recipe_id\nWHERE r.is_active = 1\nORDER BY r.created_date";

        public const string GetAllActiveOrderByCreatedDate =
            "SELECT * FROM recipes WHERE is_active=1 ORDER BY created_date";

        public const string GetAllActiveAuthorized =
            "SELECT r.*, ra.rating, fa.is_active as is_favorite\nFROM recipes r\n         LEFT JOIN (SELECT * FROM ratings WHERE is_active=1 AND user_id=@param1) ra ON r.id = ra.recipe_id\n         LEFT JOIN (SELECT * FROM favorites WHERE is_active=1 AND user_id=@param2) fa ON r.id = fa.recipe_id\nWHERE r.is_active = 1\nORDER BY r.created_date";

        public const string GetAllActiveAuthorizedCount =
            "SELECT count(*) as count FROM recipes r\n         LEFT JOIN (SELECT * FROM ratings WHERE is_active=1 AND user_id=@param1) ra ON r.id = ra.recipe_id\n         LEFT JOIN (SELECT * FROM favorites WHERE is_active=1 AND user_id=@param2) fa ON r.id = fa.recipe_id\nWHERE r.is_active = 1\nORDER BY r.created_date";

        public const string AllActiveCount =
            "SELECT count(*) as count FROM recipes WHERE is_active=1";


        public const string DeleteById = "UPDATE recipes SET is_active=0 WHERE id=@param1 AND is_active = 1";
        public const string GetByTitle = "SELECT * FROM recipes WHERE title=@param1 AND is_active=1";
        
        
    }


    public interface IFavorite
    {
        public const string Save = "INSERT INTO favorites (user_id, recipe_id) VALUES (@param1, @param2)";

        public const string DeleteById =
            "UPDATE favorites SET is_active=0 WHERE id=@param1";

        public const string GetById =
            "SELECT * FROM favorites WHERE user_id=@param1 AND is_active=1 AND recipe_id=@param2";
    }

    public interface IRating
    {
        public const string Save =
            "INSERT INTO ratings (user_id, recipe_id, rating) VALUES (@param1, @param2, @param3)";

        public const string DeleteById =
            "UPDATE ratings SET is_active=0 WHERE user_id=@param1 AND is_active = 1 AND recipe_id=@param2";

        public const string GetById =
            "SELECT * FROM ratings WHERE user_id=@param1 AND is_active=1 AND recipe_id=@param2";
    }
}