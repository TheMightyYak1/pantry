using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PantryItems.Commands;
public class DeletePantryItem
{
    public record Command (string Name) : IRequest<Result<Unit>>;

    public class Handler :IRequestHandler<Command, Result<Unit>>
    {
        private readonly IPantryDbContext _pantryDbContext;

        public Handler (IPantryDbContext pantryDbContext)
        {
            _pantryDbContext = pantryDbContext;
        }

        public async Task<Result<Unit>> Handle(Command command, CancellationToken cancellationToken)
        {
            // check if PantryItem already exists
            var pantryItem = await _pantryDbContext.PantryItems
                .Where(p => p.Name.ToLower() == command.Name.ToLower())
                .FirstOrDefaultAsync(cancellationToken);

            if (pantryItem == null) return Result<Unit>.Failure("Pantry item does not exist");

            _pantryDbContext.PantryItems.Remove(pantryItem);

            var result = await _pantryDbContext.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failed to delete pantry item");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}