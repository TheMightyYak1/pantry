using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;

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

    // public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new()) =>
    //     base.SaveChangesAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<PantryItem>(entity =>
        {
            entity.HasKey(p => p.PantryItemId);
        });

    }
}