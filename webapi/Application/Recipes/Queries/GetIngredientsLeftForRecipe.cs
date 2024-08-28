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
    public record Query(Guid UserId, Guid RecipeId) : IRequest<Result<IngredientDetailed>>;
    public class Handler : IRequestHandler<Query, Result<IngredientDetailed>>
    {
        private readonly IPantryDbContext _pantryDbContext;
        private readonly PantryItemRepository _pantryItemRepository;

        public Handler (IPantryDbContext pantryDbContext,
            PantryItemRepository pantryItemRepository)
        {
            _pantryDbContext = pantryDbContext;
            _pantryItemRepository = pantryItemRepository;
        }

        public async Task<Result<IngredientDetailed>> Handle(Query request, CancellationToken cancellationToken)
        {
            // Get Users pantryItem
            // from repo
            


            // Get recipe
            var recipe = await _pantryItemRepository.





            if (recipe == null) return Result<IngredientDetailed>.Failure("No Recipe");

            return Result<IngredientDetailed>.Success(recipe);
        }

    }
}