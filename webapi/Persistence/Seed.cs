using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Model.Ingredients;
using Microsoft.EntityFrameworkCore;

namespace Persistence;
public class Seed
{
    public static async Task SeedData(PantryDbContext pantryDbContext)
    {

        // seed users
        var user1 = new User
        (
            "PantryMan",
            "I like yummy food",
            "IMakePantryRecipes@mail.com"
        );

        var user2 = new User
        (
            "PantryWoman",
            "I like making more yummy food",
            "IMakePantryRecipesToo@mail.com"
        );

        // checks if any users
        if (! await pantryDbContext.Users.AnyAsync())
        {
            // add users to db
            await pantryDbContext.Users.AddRangeAsync(user1, user2);
            await pantryDbContext.SaveChangesAsync();
        }

        // seed pantry items
        var pantryItem1 = new PantryItem
        (
            "Rice",
            "Staple of many meals",
            Domain.Model.Enums.PantryItemType.NonPerishable,
            Domain.Model.Enums.UnitType.Cups,
            user1.UserId
        );

        var pantryItem2 = new PantryItem
        (
            "Onion",
            "Watch out when cutting!",
            Domain.Model.Enums.PantryItemType.Vegetable,
            Domain.Model.Enums.UnitType.Quantity,
            user1.UserId
        );


        // check if any pantry items
        if (! await pantryDbContext.PantryItems.AnyAsync())
        {   
            // add pantry items to db
            await pantryDbContext.PantryItems.AddRangeAsync(pantryItem1, pantryItem2);
            await pantryDbContext.SaveChangesAsync();
        }

        // seed recipes
        var recipes = new List<Recipe>
        {
            new Recipe
            (
                "Fried Rice",
                "Yummy Chinese Dish",
                new List<Ingredient>
                {
                    new Ingredient(pantryItem1.PantryItemId, 2),
                    new Ingredient(pantryItem2.PantryItemId, 1)
                },
                user1.UserId
            )
        };

        // check if any recipes
        if (! await pantryDbContext.Recipes.AnyAsync())
        {   
            // add pantry items to db
            await pantryDbContext.Recipes.AddRangeAsync(recipes);
            await pantryDbContext.SaveChangesAsync();
        }

        // seed UserPantryItems
        var userPantryItems = new List<UserPantryItem>
        {
            new UserPantryItem
            (
                user1.UserId,
                pantryItem1.PantryItemId,
                5
            ),
            new UserPantryItem
            (
                user1.UserId,
                pantryItem2.PantryItemId,
                5
            ),
            new UserPantryItem
            (
                user2.UserId,
                pantryItem1.PantryItemId,
                5
            )
        };

        // check if any recipes
        if (! await pantryDbContext.UserPantryItems.AnyAsync())
        {   
            // add pantry items to db
            await pantryDbContext.UserPantryItems.AddRangeAsync(userPantryItems);
            await pantryDbContext.SaveChangesAsync();
        }

        return;

    }
}