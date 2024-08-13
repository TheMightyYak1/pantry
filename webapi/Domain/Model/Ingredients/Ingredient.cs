using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Model.Ingredients;
public class Ingredient
{
    public Guid PantryItemId { get; set; }
    public int Quantity { get; set; }
    public Ingredient(
        Guid pantryItemId,
        int quantity
    )
    {
        PantryItemId = pantryItemId;
        Quantity = quantity;
    }
}