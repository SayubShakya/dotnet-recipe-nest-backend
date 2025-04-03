

namespace RecipeNest.Util.Impl
{
    public class BcryptUtil : IHashingUtil
    {
        public String Hash(String input)
        {
            return BCrypt.Net.BCrypt.HashPassword(input);
        }

        public bool Verify(String hash, String input)
        {
            return BCrypt.Net.BCrypt.Verify(input, hash);
        }
    }
}
