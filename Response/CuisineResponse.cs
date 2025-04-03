using MessagePack;

namespace RecipeNest.Reponse
{
    [MessagePackObject]
    public class CuisineResponse
    {
        [Key("id")]
        public int Id { get; set; }

        [Key("name")]
        public string Name { get; set; }

        [Key("image_url")]
        public string? ImageUrl { get; set; } 

        public CuisineResponse()
        {
        }

        // Parameterized constructor for easy creation in the service layer
        public CuisineResponse(int id, string name, string? imageUrl)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
        }

        public override string ToString()
        {
            return $"CuisineResponse(Id={Id}, Name='{Name}', ImageUrl='{ImageUrl}";
        }
    }
}