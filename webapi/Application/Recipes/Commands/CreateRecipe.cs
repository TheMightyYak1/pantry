using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Core;
using Domain.Entities;
using Domain.Model.Ingredients;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Recipes.Commands;
public class CreateRecipe
{
    public class Command : IRequest<Result<Recipe>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public Guid UserId { get; set; }
        public Command(string name, string description, List<Ingredient> ingredients, Guid userId)
        {
            Name = name;
            Description = description;
            Ingredients = ingredients;
            UserId = userId;
        }
    }

    public class Handler : IRequestHandler<Command, Result<Recipe>>
    {
        private readonly IPantryDbContext _pantryDbContext;

        public Handler (IPantryDbContext pantryDbContext)
        {
            _pantryDbContext = pantryDbContext;
        }
        public async Task<Result<Recipe>> Handle(Command command, CancellationToken cancellationToken)
        {
            // TODO: check if User exists

            // check if Recipe already exists by User
            var recipeUserExists = await _pantryDbContext.Recipes
                .AnyAsync(r =>
                    r.Name.ToLower() == command.Name.ToLower() &&
                    r.CreatorId == command.UserId);

            if (recipeUserExists) return Result<Recipe>.Failure("User already has recipe");

            var newRecipe = new Recipe
            (
                command.Name,
                command.Description,
                command.Ingredients,
                command.UserId
            );

            _pantryDbContext.Recipes.Add(newRecipe);

            var result = await _pantryDbContext.SaveChangesAsync() > 0;

            if (!result) return Result<Recipe>.Failure("Failed to create recipe");

            return Result<Recipe>.Success(newRecipe);
        }
    }
}