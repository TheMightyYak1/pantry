using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Enums;

namespace Application.Shared;
public record RecipeDetailed(
    Guid RecipeId,
    string Name,
    string Description,
    List<IngredientDetailed> Ingredients,
    string CreatorName);