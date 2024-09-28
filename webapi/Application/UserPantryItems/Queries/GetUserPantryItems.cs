using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Core;
using Application.Shared;
using Domain.Entities;
using MediatR;

namespace Application.UserPantryItems.Queries;
public class GetUserPantryItems
{
    public record Query(Guid UserId) : IRequest<Result<List<IngredientDetailed>>>;

    public class Handler : IRequestHandler<Query, Result<List<IngredientDetailed>>>
    {
        private readonly PantryItemRepository _pantryItemRepository;

        public Handler (PantryItemRepository pantryItemRepository)
        {
            _pantryItemRepository = pantryItemRepository;
        }

        public async Task<Result<List<IngredientDetailed>>> Handle(Query request, CancellationToken cancellationToken)
        {
            // Gets a list of all user pantry items
            // Adds quantities of like types
            
            var userPantryItem = await _pantryItemRepository.GetUsersPantryItems(request.UserId, cancellationToken);


            return Result<List<IngredientDetailed>>.Success(userPantryItem);
        }
    }

}