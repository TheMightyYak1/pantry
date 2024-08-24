using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Enums;

namespace Application.Shared;
public class RecipeDetailed
{
    public Guid RecipeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<IngredientDetailed> Ingredients { get; set; }
    public string CreatorName { get; set; }

    public RecipeDetailed(Guid recipeId, string name, string description, List<IngredientDetailed> ingredients, string creatorName)
    {
        RecipeId = recipeId;
        Name = name;
        Description = description;
        Ingredients = ingredients;
        CreatorName = creatorName;
    }
}

public record IngredientDetailed(Guid PantryItemId, string Name, string Description, int Quantity, UnitType UnitType);