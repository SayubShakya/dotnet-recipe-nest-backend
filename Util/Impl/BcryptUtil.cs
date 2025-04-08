namespace RecipeNest.Util.Impl;

public class BcryptUtil : IHashingUtil
{
    public string Hash(string input)
    {
        return BCrypt.Net.BCrypt.HashPassword(input);
    }

    public bool Verify(string hash, string input)
    {
        return BCrypt.Net.BCrypt.Verify(input, hash);
    }
}