using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Core;
using Application.Recipes.DTOs;
using Domain.Entities;
using Domain.Model.Ingredients;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Recipes.Queries;
public class GetRecipe
{
    public record Query(Guid Id) : IRequest<Result<RecipeDto>>;
    public class Handler : IRequestHandler<Query, Result<RecipeDto>>
    {
        private readonly IPantryDbContext _pantryDbContext;

        public Handler (IPantryDbContext pantryDbContext)
        {
            _pantryDbContext = pantryDbContext;
        }

        public async Task<Result<RecipeDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            // Get recipe from Db
            var recipe = await _pantryDbContext.Recipes
                .Select(r => r.RecipeId == request.Id)
                .Select();

            if (recipe == null) return Result<RecipeDto>.Failure("No Recipe");



        

            // Get List PantryItem details
            var ingredients = await _pantryDbContext.PantryItems
                .Select(i => i.PantryItemId == recipe.Ingredients.)
            
            recipe.Ingredients
                .Select(i => i.PantryItemId == );

            var recipes = await _pantryDbContext.Recipes
                .Select(recipe => new RecipeDto
                (
                    recipe.RecipeId,
                    recipe.Name,
                    recipe.Description,
                    recipe.Creator.Username
                ))
                .ToListAsync(cancellationToken);

            return Result<RecipeDto>.Success(recipes);
        }

        public async Task<List<PantryItem>> GetPantryItemFromIngredient (List<Ingredient> ingredients, CancellationToken cancellationToken)
        {
            var ingredientPantryItemIds = ingredients.Select(x => x.PantryItemId).ToList();

            var ingredientList = await _pantryDbContext.PantryItems
                .Where(p => ingredientPantryItemIds.Contains(p.PantryItemId))
                .ToListAsync();

            return;
        }
    }
}