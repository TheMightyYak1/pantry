using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Recipe
{
    public Guid RecipeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Ingredient> Ingredients { get; set; }

    public Recipe(string name, string Description, List<Ingredient> ingredients)
    {
        RecipeId = Guid.NewGuid();
        Name = name;
        Ingredients = ingredients;
    }
}

public record Ingredient (Guid PantryItemId, int Quantity);