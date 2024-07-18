using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Domain.Entities;
using MediatR;

namespace Application.PantryItems.Commands;
public class CreatePantryItem
{
    public class Command : IRequest<Result<PantryItem>>
    {
        public Command()
        {

        }
    }

    public class Handler :IRequestHandler<Command, Result<PantryItem>>
    {
        public Handler()
        {

        }

        public async Task<Result<PantryItem>> Handle(Command command, CancellationToken cancellationToken)
        {
            //var newPantryItem = 

            throw new NotImplementedException();

        }
    }
}
