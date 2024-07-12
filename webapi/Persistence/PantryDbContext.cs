using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;
public class PantryDbContext
{
    private readonly string _connectionString;


    public PantryDbContext()
    {
    }

    public PantryDbContext(DbContextOptions options) : base(options)
    {

    }

    public PantryDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<PantryItem> PantryItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<PantryItem>(entity =>
        {
            entity.HasKey(p => p.PantryItemId);
        });

    }
}