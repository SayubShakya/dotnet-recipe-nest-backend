using RecipeNest.Model;

namespace RecipeNest.Dto;

public class SessionUserDTO
{
    public User? User
    {
        get => user;
        set => user = value;
    }

    private User? user;
    private bool authenticated;

    public bool Authenticated
    {
        get => authenticated;
        set => authenticated = value;
    }
}