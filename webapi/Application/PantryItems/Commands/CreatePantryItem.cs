using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Application.Core;
using Domain.Entities;
using Domain.Model.Enums;
using MediatR;

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
        public Handler()
        {

        }

        public async Task<Result<PantryItem>> Handle(Command command, CancellationToken cancellationToken)
        {
            var newPantryItem = new PantryItem
            (
                command.Name,
                command.Description,
                command.PantryItemType,
                command.UnitType
            );


            // var result = await _context.SaveChangesAsync() > 0;

            //if (!result) return Result<Unit>.Failure("Failed to create activity");


            throw new NotImplementedException();

        }
    }
}
