using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;
public class Seed
{
    public static async Task SeedData(PantryDbContext pantryDbContext)
    {
        // returns if there are any pantry items

        var users = new List<User>
        {
            new User
            (
                "PantryMan",
                "IMakePantryRecipes@mail.com"
            ),
            new User
            (
                "PantryWoman",
                "IMakePantryRecipesToo@mail.com"
            )
        };

        if (! await pantryDbContext.Users.AnyAsync())
        {
            // add users to db
            await pantryDbContext.Users.AddRangeAsync(users);
            await pantryDbContext.SaveChangesAsync();
        }

        if (pantryDbContext.PantryItems.Any()) return;

        var pantryItems = new List<PantryItem>
        {
            new PantryItem
            (
                "Rice",
                "Staple of many meals",
                Domain.Model.Enums.PantryItemType.NonPerishable,
                Domain.Model.Enums.UnitType.kg,
                users.FirstOrDefault().UserId
            )
        };

        await pantryDbContext.PantryItems.AddRangeAsync(pantryItems);
        await pantryDbContext.SaveChangesAsync();

    }
}