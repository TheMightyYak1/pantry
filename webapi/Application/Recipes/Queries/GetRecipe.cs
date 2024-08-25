using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Core;
using Application.Recipes.DTOs;
using Application.Shared;
using Domain.Entities;
using Domain.Model.Ingredients;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Recipes.Queries;
public class GetRecipe
{
    public record Query(Guid Id) : IRequest<Result<RecipeDetailed>>;
    public class Handler : IRequestHandler<Query, Result<RecipeDetailed>>
    {
        private readonly IPantryDbContext _pantryDbContext;
        private readonly PantryItemRepository _pantryItemRepository;

        public Handler (IPantryDbContext pantryDbContext,
            PantryItemRepository pantryItemRepository)
        {
            _pantryDbContext = pantryDbContext;
            _pantryItemRepository = pantryItemRepository;
        }

        public async Task<Result<RecipeDetailed>> Handle(Query request, CancellationToken cancellationToken)
        {
            // Get recipe 
            var recipe = _pantryItemRepository.GetRecipeDetailed(request.Id, cancellationToken);

            if (recipe.Result == null) return Result<RecipeDetailed>.Failure("No Recipe");

            return Result<RecipeDetailed>.Success(recipe.Result);
        }

    }
}