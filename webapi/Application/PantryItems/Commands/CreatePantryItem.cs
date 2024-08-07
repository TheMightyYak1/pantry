using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Core;
using Domain.Entities;
using Domain.Model.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PantryItems.Commands;
public class CreatePantryItem
{
    public class Command : IRequest<Result<PantryItem>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PantryItemType PantryItemType { get; set; }
        public UnitType UnitType { get; set; }

        public Command(string name, string description, PantryItemType pantryItemType, UnitType unitType)
        {
            Name = name;
            Description = description;
            PantryItemType = pantryItemType;
            UnitType = unitType;
        }
    }

    public class Handler :IRequestHandler<Command, Result<PantryItem>>
    {
        private readonly IPantryDbContext _pantryDbContext;

        public Handler(
            IMediator mediator,
            IPantryDbContext pantryDbContext)
        {
            _pantryDbContext = pantryDbContext;
        }

        public async Task<Result<PantryItem>> Handle(Command command, CancellationToken cancellationToken)
        {
            // check if PantryItem already exists
            var ifPantryItemExists = await _pantryDbContext.PantryItems
                .Where(p => p.Name.ToLower() == command.Name.ToLower())
                .FirstOrDefaultAsync(cancellationToken);

            if (ifPantryItemExists != null) return Result<PantryItem>.Failure("Pantry item already exists");

            var newPantryItem = new PantryItem
            (
                command.Name,
                command.Description,
                command.PantryItemType,
                command.UnitType
            );

            _pantryDbContext.PantryItems.Add(newPantryItem);

            var result = await _pantryDbContext.SaveChangesAsync() > 0;

            if (!result) return Result<PantryItem>.Failure("Failed to create pantry item");

            return Result<PantryItem>.Success(newPantryItem);
        }
    }
}
