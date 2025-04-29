using RecipeNest.Entity;

namespace RecipeNest.Dto;

public class SessionUser
{
    private User? user;
    private Role? role;
    private bool authenticated;

    public User? User
    {
        get => user;
        set => user = value;
    }

    public Role? Role
    {
        get => role;
        set => role = value;
    }

    public bool Authenticated
    {
        get => authenticated;
        set => authenticated = value;
    }
}