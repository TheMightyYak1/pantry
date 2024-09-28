using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.UserPantryItems.Commands;
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

    /// <summary>
    ///     Adds a User Pantry Item.
    ///     Can be both positive and negative amounts.
    /// </summary>
    [HttpPost("{userId}")]
    public async Task<ActionResult> AddUserPantryItem([FromRoute] Guid UserId, [FromBody] Guid PantryItemId, int quantity)
    {
        return HandleResult(await Mediator.Send(new AddUserPantryItem.Command(UserId, PantryItemId, quantity)));
    }

    /// <summary>
    ///     Bulk adds User Pantry Item
    /// </summary>
    
}