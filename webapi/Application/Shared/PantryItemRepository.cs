using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Model.Ingredients;
using Microsoft.EntityFrameworkCore;

namespace Application.Shared;
public class PantryItemRepository
{
    private readonly IPantryDbContext _pantryDbContext;

    public PantryItemRepository(IPantryDbContext pantryDbContext)
    {
        _pantryDbContext = pantryDbContext;
    }


    public async Task<RecipeDetailed> GetRecipeDetailed(Guid recipeId, CancellationToken cancellationToken)
    {
        

    }

    public async Task<List<IngredientDetailed>> GetListIngredientDetailed (List<Ingredient> ingredients, CancellationToken cancellationToken)
    {
        // TODO: check if 0 ingredients

        var ingredientPantryItemIds = ingredients.Select(i => i.PantryItemId).ToList();

        var ingredientsDetailed = await _pantryDbContext.PantryItems
            .Where(p => ingredientPantryItemIds.Contains(p.PantryItemId))
            .Select(pantryItem => new IngredientDetailed
            (
                pantryItem.PantryItemId,
                pantryItem.Name,
                pantryItem.Description,
                // potentiall use a join function to optimise
                ingredients.First(i => i.PantryItemId == pantryItem.PantryItemId).Quantity,
                pantryItem.UnitType
            ))
            .ToListAsync(cancellationToken);

            return ingredientsDetailed;
    }

}