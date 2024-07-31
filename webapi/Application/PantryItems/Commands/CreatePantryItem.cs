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
        private readonly IMediator _mediator;

        public Handler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<PantryItem>> Handle(Command command, CancellationToken cancellationToken)
        {
            // check if Pantry Item exists

            // var operativeConnectedWallet = await _DbContext.Operatives
            //     .Where(o => o.OperativeId == command.OperativeId)
            //     .Select(o => o.ConnectedWalletAddress)
            //     .FirstOrDefaultAsync(cancellationToken);

            // // need to check for case sensitivity
            // var ifPantryItemExists = await _pantryDbContext.PantryItems
            //     .Where(p => p.Name == command.Name)
            //     .FirstOrDefaultAsync(cancellationToken);

            // if (ifPantryItemExists != null)
            // {
            //     // TODO: Throw proper typed exception
            //     throw new Exception("Pantry Item aleady exists");
            // }

            var newPantryItem = new PantryItem
            (
                command.Name,
                command.Description,
                command.PantryItemType,
                command.UnitType
            );

            // _pantryDbContext.PantryItems.Add(newPantryItem);

            // var result = await _pantryDbContext.SaveChangesAsync() > 0;

           // if (!result) return Result<PantryItem>.Failure("Failed to create activity");

            return Result<PantryItem>.Success(newPantryItem);
        }
    }
}
