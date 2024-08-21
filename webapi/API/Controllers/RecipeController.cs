using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Recipes.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class RecipeController : BaseApiController
{
    /// <summary>
    ///     Gets a list of all recipes
    /// </summary>
    [HttpGet]
    public async Task<ActionResult> GetRecipes()
    {
        return HandleResult(await Mediator.Send(new GetRecipes.Query()));
    }

    
}