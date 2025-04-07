namespace RecipeNest.Util
{
    public interface IHashingUtil
    {
        String Hash(String input);

        bool Verify(String hash, String input);
    }
}