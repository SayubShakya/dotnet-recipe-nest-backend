// Cuisine.cs

using System;
using MessagePack;

namespace RecipeNest.Model
{
    [MessagePackObject]
    public class Cuisine
    {
        [Key("id")] public int Id { get; set; }

        [Key("name")] public string Name { get; set; }

        [Key("image_url")] public string? ImageUrl { get; set; }

        public Cuisine()
        {
        }

        public Cuisine(int id, string name, string? imageUrl)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
        }

        public override string ToString()
        {
            return $"Cuisine ID: {Id}, Name: {Name}, ImageUrl: {ImageUrl}";
        }
    }
}