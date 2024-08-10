using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.PantryItems;
using Application.PantryItems.Queries;
using Application.PantryItems.Commands;
using Domain.Entities;

namespace API.Controllers;
public class PantryItemsController : BaseApiController
{
    /// <summary>
    ///     Gets a list of all pantry items
    /// </summary>
    [HttpGet]
    public async Task<ActionResult> GetPantryItems()
    {
        return HandleResult(await Mediator.Send(new GetPantryItems.Query()));
    }

    /// <summary>
    ///     Gets information for a pantry item
    /// </summary>
    [HttpGet("{name}")] // pantryItems/id
    public async Task<ActionResult> GetPantryItem([FromRoute] string name)
    {
        return HandleResult(await Mediator.Send(new GetPantryItem.Query(name)));
    }

    /// <summary>
    ///     Add a new pantry item, and return the create pantry item
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PantryItem>> CreatePantryItem([FromBody] CreatePantryItem.Command command)
    {
        return HandleResult(await Mediator.Send(command));
    }


}