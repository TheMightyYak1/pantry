using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;

namespace Application.Shared;
public class PantryItemRepository
{
    private readonly IPantryDbContext _pantryDbContext;

    public PantryItemRepository(IPantryDbContext pantryDbContext)
    {
        _pantryDbContext = pantryDbContext;
    }


    public async Task<RecipeDetailed> GetRecipeDetailed(Guid recipeId, CancellationToken cancellationToken)
    {
        



    }

}