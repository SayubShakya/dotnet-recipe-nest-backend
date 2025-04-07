// RoleResponse.cs

using MessagePack;

namespace RecipeNest.Reponse
{
    [MessagePackObject]
    public class RoleResponse
    {
        [Key("id")] public int Id { get; set; }

        [Key("name")] public string Name { get; set; }

        public RoleResponse()
        {
        }

        public RoleResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"Role ID: {Id}, Name: {Name}";
        }
    }
}