using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Recipes.DTOs;
public record RecipeDto(

    Guid RecipeId,
    string Name,
    string Description,
    string CreatorUsername
);