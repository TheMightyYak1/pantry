using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.PantryItems;
using Application.PantryItems.Queries;

namespace API.Controllers;
public class PantryItemsController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetPantryItems()
    {
        return HandleResult(await Mediator.Send(new GetPantryItems.Query()));
    }

    

}