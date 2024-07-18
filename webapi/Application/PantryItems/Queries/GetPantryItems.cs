using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using MediatR;

namespace Application.PantryItems.Queries;
public class GetPantryItems
{
    public class Query : IRequest<Result<Unit>>
    {
        
    }

    public class Handler : IRequestHandler<Query, Result<Unit>>
    {
        // private readonly DbContent

        public Handler()
        {
        
        }

        public Task<Result<Unit>> Handle(Query request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}   
