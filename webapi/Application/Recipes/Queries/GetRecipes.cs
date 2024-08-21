using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Core;
using Application.Recipes.DTOs;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Recipes.Queries;
public class GetRecipes
{
    public class Query : IRequest<Result<List<RecipeDto>>>
    {
        
    }
    public class Handler : IRequestHandler<Query, Result<List<RecipeDto>>>
    {
        private readonly IPantryDbContext _pantryDbContext;

        public Handler (IPantryDbContext pantryDbContext)
        {
            _pantryDbContext = pantryDbContext;
        }

        public async Task<Result<List<RecipeDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var recipes = await _pantryDbContext.Recipes
                .Select(recipe => new RecipeDto
                (
                    recipe.RecipeId,
                    recipe.Name,
                    recipe.Description,
                    recipe.Creator.Username
                ))
                .ToListAsync(cancellationToken);

            if (recipes == null) return Result<List<RecipeDto>>.Failure("No Recipes");

            return Result<List<RecipeDto>>.Success(recipes);
        }
    }
}