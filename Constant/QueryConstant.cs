//QueryConstant.cs

namespace RecipeNest.Consta
{
    public interface IQueryConstant
    {
        public interface IRole
        {
            public const string SAVE = "INSERT INTO roles(name) VALUES (@param1)";
            public const string UPDATE = "UPDATE roles SET name=@param1 WHERE is_active=1 and id=@param2";
            public const string GET_BY_ID = "SELECT * FROM roles WHERE id=@param1 and is_active=1";
            public const string GET_ALL = "SELECT * FROM roles WHERE is_active=1";
            public const string DELETE_BY_ID = "UPDATE roles set is_active=0 WHERE id=@param1 And is_active = 1";
        }

        public interface IUser
        {
            public const string SAVE =
                " INSERT INTO users (first_name, last_name, phone_number, image_url, about, email, password, role_id) VALUES ( @param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8)";

            public const string UPDATE =
                "UPDATE users SET first_name=@param1, last_name=@param2, phone_number=@param3,image_url=@param4, about=@param5, email=@param6, password=@param7, role_id=@param8 WHERE is_active=1 AND id=@param9";

            public const string GET_BY_ID = "SELECT * FROM users WHERE id=@param1 AND is_active=1";
            public const string GET_ALL = "SELECT * FROM users WHERE is_active=1";
            public const string DELETE_BY_ID = "UPDATE users SET is_active=0 WHERE id=@param1 AND is_active = 1";
            public const string GET_BY_EMAIL = "SELECT * FROM users WHERE email=@param1 AND is_active=1";
        }

        public interface ICuisine
        {
            public const string SAVE = "INSERT INTO cuisines (name, image_url) VALUES (@param1, @param2)";

            public const string UPDATE =
                "UPDATE cuisines SET name=@param1, image_url=@param2 WHERE is_active=1 AND id=@param3";

            public const string GET_BY_ID = "SELECT * FROM cuisines WHERE id=@param1 AND is_active=1";
            public const string GET_ALL = "SELECT * FROM cuisines WHERE is_active=1";
            public const string DELETE_BY_ID = "UPDATE cuisines SET is_active=0 WHERE id=@param1 AND is_active = 1";
            public const string GET_BY_NAME = "SELECT * FROM cuisines WHERE name=@param1 AND is_active=1";
        }

        public interface IRecipe
        {
            public const string SAVE =
                "INSERT INTO recipes (image_url, title, description, recipe, ingredients, recipe_by, cuisine) VALUES (@param1, @param2, @param3, @param4, @param5, @param6, @param7)";

            public const string UPDATE =
                "UPDATE recipes SET image_url=@param1, title=@param2, description=@param3, recipe=@param4, ingredients=@param5, recipe_by=@param6, cuisine=@param7 WHERE is_active=1 AND id=@param8";

            public const string GET_BY_ID = "SELECT * FROM recipes WHERE id=@param1 AND is_active=1";

            public const string GET_ALL =
                "SELECT * FROM recipes r LEFT JOIN favorites f ON r.id=f.recipe_id WHERE r.is_active=1 AND f.is_active=1";

            public const string DELETE_BY_ID = "UPDATE recipes SET is_active=0 WHERE id=@param1 AND is_active = 1";
            public const string GET_BY_TITLE = "SELECT * FROM recipes WHERE title=@param1 AND is_active=1";

            public const string GET_ALL_FAVORITES =
                "SELECT * FROM recipes r INNER JOIN favorites f ON r.id=f.recipe_id WHERE r.is_active=1 AND f.is_active=1 AND f.user_id=@param1";
        }


        public interface IFavorite
        {
            public const string SAVE = "INSERT INTO favorites (user_id, recipe_id) VALUES (@param1, @param2)";

            public const string DELETE_BY_ID =
                "UPDATE favorites SET is_active=0 WHERE user_id=@param1 AND is_active = 1 AND recipe_id=@param2";

            public const string GET_BY_ID =
                "SELECT * FROM favorites WHERE user_id=@param1 AND is_active=1 AND recipe_id=@param2";
        }

        public interface IRating
        {
            public const string SAVE =
                "INSERT INTO ratings (user_id, recipe_id, rating) VALUES (@param1, @param2, @param3)";

            public const string DELETE_BY_ID =
                "UPDATE ratings SET is_active=0 WHERE user_id=@param1 AND is_active = 1 AND recipe_id=@param2";

            public const string GET_BY_ID =
                "SELECT * FROM ratings WHERE user_id=@param1 AND is_active=1 AND recipe_id=@param2";
        }
    }
}