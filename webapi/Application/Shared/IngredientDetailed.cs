using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Enums;

namespace Application.Shared;

public record IngredientDetailed(
    Guid PantryItemId,
    string Name,
    string Description,
    int Quantity,
    UnitType UnitType);