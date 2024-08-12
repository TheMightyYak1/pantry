using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Recipe
{
    public Guid RecipeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Ingredient> Ingredients { get; set; }

    [JsonIgnore]
    public User Creator { get; set; }

    public Recipe(string name, string description, List<Ingredient> ingredients, User creator)
    {
        RecipeId = Guid.NewGuid();
        Name = name;
        Description = description;
        Ingredients = ingredients;
        Creator = creator;
    }
}

public record Ingredient (Guid PantryItemId, int Quantity);