using MessagePack;

namespace RecipeNest.Request;

[MessagePackObject]
public class ToggleUserStatusRequest
{
    public ToggleUserStatusRequest()
    {
    }


    public ToggleUserStatusRequest(int id, bool isActive)
    {
        Id = id;
        IsActive = isActive;
    }

    [Key("id")] public int Id { get; set; }

    [Key("is_active")] public bool IsActive { get; set; }
}