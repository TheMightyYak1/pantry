using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Core;
using Application.Shared;
using Domain.Entities;
using MediatR;

namespace Application.Recipes.Queries;
public class GetRecipesUserCanMake
{
    public record Query(Guid UserId) : IRequest<Result<List<Recipe>>>;
    public class Handler : IRequestHandler<Query, Result<List<Recipe>>>
    {
        private readonly IPantryDbContext _pantryDbContext;
        private readonly PantryItemRepository _pantryItemRepository;

        public Handler (IPantryDbContext pantryDbContext,
            PantryItemRepository pantryItemRepository)
        {
            _pantryDbContext = pantryDbContext;
            _pantryItemRepository = pantryItemRepository;
        }

        public async Task<Result<List<Recipe>>> Handle(Query request, CancellationToken cancellationToken)
        {
            // Get Users pantryItem
            var usersPantryItems = await _pantryItemRepository.GetUsersPantryItems(request.UserId, cancellationToken);

            // Get recipes  
            var recipes = await _pantryItemRepository.RecipesUserCanMake(usersPantryItems, cancellationToken);

            if (recipes == null) return Result<List<Recipe>>.Failure("You can't make any recipes");

            return Result<List<Recipe>>.Success(recipes);
        }

    }
}