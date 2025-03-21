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
    }
}