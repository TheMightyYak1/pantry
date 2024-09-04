using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Core;
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
        // TODO: handle errors

        // Optional: can combine the LINQ query for 1 db query to optimise

        var recipe = await _pantryDbContext.Recipes
            .Include(r => r.Creator)
            .FirstOrDefaultAsync(r => r.RecipeId == recipeId);

        var ingredientsDetailed = GetListIngredientDetailed(recipe.Ingredients, cancellationToken);

        var recipeDetailed = new RecipeDetailed
        (
            recipe.RecipeId,
            recipe.Name,
            recipe.Description,
            ingredientsDetailed.Result,
            // recipe.Ingredients.Join(
            //     _pantryDbContext.PantryItems,
            //     ingredient => ingredient.PantryItemId,
            //     pantryItem => pantryItem.PantryItemId,
            //     (ingredient, pantryItem) => new IngredientDetailed
            //     (
            //         pantryItem.PantryItemId,
            //         pantryItem.Name,
            //         pantryItem.Description,
            //         ingredient.Quantity,
            //         pantryItem.UnitType
            //     )
            // ).ToList(),
            recipe.Creator.Username
        );

        return recipeDetailed;
    }

    public async Task<List<IngredientDetailed>> GetListIngredientDetailed (List<Ingredient> ingredients, CancellationToken cancellationToken)
    {
        // TODO: check if 0 ingredients

        //var ingredientPantryItemIds = ingredients.Select(i => i.PantryItemId).ToList();

        var ingredientQuantities = ingredients.ToDictionary(i => i.PantryItemId, i => i.Quantity);

        var pantryItemsDetailed = await _pantryDbContext.PantryItems
            .Where(p => ingredientQuantities.Keys.Contains(p.PantryItemId))
            .ToListAsync(cancellationToken);

        var ingredientsDetailed = pantryItemsDetailed.Select(
            pantryItem => new IngredientDetailed
            (
                pantryItem.PantryItemId,
                pantryItem.Name,
                pantryItem.Description,
                // potential use a join function to optimise
                ingredientQuantities[pantryItem.PantryItemId],
                pantryItem.UnitType
            ))
            .ToList();

        return ingredientsDetailed;
    }

    public async Task<List<IngredientDetailed>> GetUsersPantryItems (Guid UserId, CancellationToken cancellationToken)
    {
        var usersPantryItems = await _pantryDbContext.UserPantryItems
            .Where(p => p.UserId == UserId)
            .Select(pantryItem => new IngredientDetailed
            (
                pantryItem.PantryItemId,
                pantryItem.PantryItem.Name,
                pantryItem.PantryItem.Description,
                pantryItem.Quantity,
                pantryItem.PantryItem.UnitType
            ))
            .ToListAsync(cancellationToken);

        return usersPantryItems;
    }

    // TODO: check if it works
    public async Task<List<IngredientDetailed>> AdditionalIngredientsForRecipe(
        RecipeDetailed recipe,
        List<IngredientDetailed> userIngredients,
        CancellationToken cancellationToken)
    {
        // Convert users ingredients to a dictionary for fast lookup
        var userIngredientsDict = userIngredients.ToDictionary(ui => ui.PantryItemId);

        // Builds additional ingredient list
        var additionalIngredients = recipe.Ingredients
            .Select(recipeIngredient => 
            {
                // Check if the ingredient is available in the user's pantry
                var userIngredientExists = userIngredientsDict.TryGetValue(recipeIngredient.PantryItemId, out var userIngredient);
                
                // Calculate required additional quantity of ingredients
                var additionalQuantity = recipeIngredient.Quantity - (userIngredientExists ? userIngredient.Quantity : 0);

                return new IngredientDetailed(
                recipeIngredient.PantryItemId,
                recipeIngredient.Name,
                recipeIngredient.Description,
                additionalQuantity > 0 ? additionalQuantity : 0, // Only add if more is needed
                recipeIngredient.UnitType );
            })
            .Where(i => i.Quantity > 0)
            .ToList();

            return additionalIngredients;
    } 

}