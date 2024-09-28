using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Core;
using Domain.Entities;
using MediatR;

namespace Application.UserPantryItems.Commands;
public class AddUserPantryItem
{
    public record Command(Guid UserId, Guid PantryItemId, int Quantity) : IRequest<Result<UserPantryItem>>;
    public class Handler : IRequestHandler<Command, Result<UserPantryItem>>
    {
        private readonly IPantryDbContext _pantryDbContext;

        public Handler (IPantryDbContext pantryDbContext)
        {
            _pantryDbContext = pantryDbContext;
        }

        public async Task<Result<UserPantryItem>> Handle(Command command, CancellationToken cancellationToken)
        {
            var newUserPantryItem = new UserPantryItem
            (
                command.UserId,
                command.PantryItemId,
                command.Quantity
            );

            _pantryDbContext.UserPantryItems.Add(newUserPantryItem);

            var result = await _pantryDbContext.SaveChangesAsync() > 0;

            if (!result) return Result<UserPantryItem>.Failure("Failed to create user pantry item");

            return Result<UserPantryItem>.Success(newUserPantryItem);
        }

    }
}