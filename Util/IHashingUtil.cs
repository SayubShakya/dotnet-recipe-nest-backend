namespace RecipeNest.Util;

public interface IHashingUtil
{
    string Hash(string input);

    bool Verify(string hash, string input);
}