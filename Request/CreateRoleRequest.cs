using MessagePack;

namespace RecipeNest.Request
{
    [MessagePackObject]
    public class CreateRoleRequest
    {

        [Key("name")]
        public string Name { get; set; }

        public CreateRoleRequest() { }

        public CreateRoleRequest(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"Role, Name: {Name}";
        }
    }
}
