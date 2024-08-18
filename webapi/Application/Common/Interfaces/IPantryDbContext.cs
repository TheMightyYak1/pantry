using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;
public interface IPantryDbContext
{
    DbSet<PantryItem> PantryItems { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Recipe> Recipes { get; set; }
    DbSet<UserPantryItem> UserPantryItems { get; set; }

    // DbSet<TEntity> Set<TEntity>() where TEntity : class;

    // EntityEntry Entry(object entity);
    // EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());

}