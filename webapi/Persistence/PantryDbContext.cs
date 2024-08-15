using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Domain.Model.Ingredients;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence;
public class PantryDbContext : DbContext, IPantryDbContext
{
    public PantryDbContext()
    {
    }

    public PantryDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<PantryItem> PantryItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Recipe> Recipes { get; set; }

    // public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new()) =>
    //     base.SaveChangesAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // remove if sqllite db changed to postgres
        var ingredientComparer = new ValueComparer<List<Ingredient>>(
            (c1, c2) => c1.SequenceEqual(c2), // Compare lists by sequence equality
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.PantryItemId.GetHashCode())), // Hash code generation
            c => c.ToList() // Deep copy of the list
        );

        builder.Entity<PantryItem>(entity =>
        {
            entity.HasKey(p => p.PantryItemId);
            entity.HasOne(r => r.Creator).WithMany(u => u.PantryItems).HasForeignKey(r => r.CreatorId);

        });

        builder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserId);
        });

        builder.Entity<Recipe>(entity =>
        {
            entity.HasKey(r => r.RecipeId);
            entity.HasOne(r => r.Creator).WithMany(u => u.Recipes).HasForeignKey(r => r.CreatorId);
            
            entity.Property(r => r.Ingredients)
                // remove if sqllite db changed to postgres
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<Ingredient>>(v, (System.Text.Json.JsonSerializerOptions)null))
                    .HasColumnType("TEXT")
                    .Metadata.SetValueComparer(ingredientComparer); // Apply the ValueComparer
                
            // add in when/if change to postgres
            // entity.Property(r => r.Ingredients).HasColumnType("jsonb");
        });

    }
}