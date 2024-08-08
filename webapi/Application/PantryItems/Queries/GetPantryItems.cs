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
public class GetPantryItems
{
    public class Query : IRequest<Result<List<PantryItem>>>
    {
        
    }

    public class Handler : IRequestHandler<Query, Result<List<PantryItem>>>
    {
        private readonly IPantryDbContext _pantryDbContext;

        public Handler (IPantryDbContext pantryDbContext)
        {
            _pantryDbContext = pantryDbContext;
        }

        public async Task<Result<List<PantryItem>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var pantryItems = await _pantryDbContext.PantryItems
                .ToListAsync(cancellationToken);

            if (pantryItems == null) return Result<List<PantryItem>>.Failure("No Pantry Items");

            return Result<List<PantryItem>>.Success(pantryItems);
        }
    }
}   
