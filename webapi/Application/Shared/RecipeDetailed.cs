using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Enums;

namespace Application.Shared;
public record RecipeDetailed(
    Guid recipeId,
    string name,
    string description,
    List<IngredientDetailed> ingredients,
    string creatorName);