using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Domain.Model.Ingredients;

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
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<Ingredient>>(v, (System.Text.Json.JsonSerializerOptions)null))
                    .HasColumnType("TEXT");
                
            // add in when/if change to postgres
            // entity.Property(r => r.Ingredients).HasColumnType("jsonb");
        });

    }
}