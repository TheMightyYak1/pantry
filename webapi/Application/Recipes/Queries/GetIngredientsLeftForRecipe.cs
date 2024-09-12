using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Core;
using Application.Shared;
using MediatR;

namespace Application.Recipes.Queries;
public class GetIngredientsLeftForRecipe
{
    public record Query(Guid UserId, Guid RecipeId) : IRequest<Result<List<IngredientDetailed>>>;
    public class Handler : IRequestHandler<Query, Result<List<IngredientDetailed>>>
    {
        private readonly IPantryDbContext _pantryDbContext;
        private readonly PantryItemRepository _pantryItemRepository;

        public Handler (IPantryDbContext pantryDbContext,
            PantryItemRepository pantryItemRepository)
        {
            _pantryDbContext = pantryDbContext;
            _pantryItemRepository = pantryItemRepository;
        }

        public async Task<Result<List<IngredientDetailed>>> Handle(Query request, CancellationToken cancellationToken)
        {
            // Get Users pantryItem
            var usersPantryItems = await _pantryItemRepository.GetUsersPantryItems(request.UserId, cancellationToken);

            // Get recipe
            var recipe = await _pantryItemRepository.GetRecipeDetailed(request.RecipeId, cancellationToken);

            var additionalIngredientsForRecipe = _pantryItemRepository.AdditionalIngredientsForRecipe(recipe, usersPantryItems);

            if (recipe == null) return Result<List<IngredientDetailed>>.Failure("No Recipe");

            return Result<List<IngredientDetailed>>.Success(additionalIngredientsForRecipe);
        }

    }
}