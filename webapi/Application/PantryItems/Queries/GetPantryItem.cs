using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Core;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PantryItems.Queries;
public class GetPantryItem
{
    public record Query(string Name) : IRequest<Result<PantryItem>>;

    public class Handler : IRequestHandler<Query, Result<PantryItem>>
    {
        private readonly IPantryDbContext _pantryDbContext;

        public Handler (IPantryDbContext pantryDbContext)
        {
            _pantryDbContext = pantryDbContext;
        }

        public async Task<Result<PantryItem>> Handle(Query request, CancellationToken cancellationToken)
        {
            // TODO: Send back DTO without ID
            // TODO: check if still works
            var pantryItem = await _pantryDbContext.PantryItems
                .FirstOrDefaultAsync(p => p.Name.Equals(request.Name, StringComparison.CurrentCultureIgnoreCase), cancellationToken);

            if (pantryItem == null) return Result<PantryItem>.Failure("No Pantry Item");

            return Result<PantryItem>.Success(pantryItem);
        }
    }
}   
