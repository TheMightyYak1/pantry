using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Recipes.Commands;
using Application.Recipes.Queries;
using Application.Shared;
using Domain.Entities;
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

    /// <summary>
    ///     Gets details for a recipe
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetRecipe([FromRoute] Guid id)
    {
        return HandleResult(await Mediator.Send(new GetRecipe.Query(id)));
    }

    /// <summary>
    ///     Add a new recipe, and return the created recipe
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] CreateRecipe.Command command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    /// <summary>
    ///     Gets ingredients user needs to make the recipe
    /// </summary>
    [HttpGet("{userId}/{recipeId}")]
    public async Task<ActionResult<List<IngredientDetailed>>> GetIngredientsLeftForRecipe([FromRoute] Guid userId, Guid recipeId) 
    {
        return HandleResult(await Mediator.Send(new GetIngredientsLeftForRecipe.Query(userId, recipeId)));
    }

    /// <summary>
    ///     Gets recipes user can make with user ingredients
    /// </summary>
    [HttpGet("recipes/{userId}")]
    public async Task<ActionResult> GetRecipesUserCanMake([FromRoute] Guid userId)
    {
        return HandleResult(await Mediator.Send(new GetRecipesUserCanMake.Query(userId)));
    }

}