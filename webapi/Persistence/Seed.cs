using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Domain.Entities;

namespace Persistence;
public class Seed
{
    public static async Task SeedData(PantryDbContext pantryDbContext)
    {
        // returns if there are any pantry items
        if (pantryDbContext.PantryItems.Any()) return;

        var pantryItems = new List<PantryItem>
        {
            new PantryItem
            (
                "Rice",
                "Staple of many meals",
                Domain.Model.Enums.PantryItemType.NonPerishable,
                Domain.Model.Enums.UnitType.kg
            )
        };

        await pantryDbContext.PantryItems.AddRangeAsync(pantryItems);
        await pantryDbContext.SaveChangesAsync();

    }
}