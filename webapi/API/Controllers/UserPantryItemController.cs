using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.UserPantryItems.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class UserPantryItemController : BaseApiController
{
    /// <summary>
    ///     Gets a list of users pantry items
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetUserPantryItems([FromRoute] Guid id)
    {
        return HandleResult(await Mediator.Send(new GetUserPantryItems.Query(id)));
    }
}