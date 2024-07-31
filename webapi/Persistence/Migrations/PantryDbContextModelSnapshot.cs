﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(PantryDbContext))]
    partial class PantryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("Domain.Entities.PantryItem", b =>
                {
                    b.Property<Guid>("PantryItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("PantryItemType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UnitType")
                        .HasColumnType("INTEGER");

                    b.HasKey("PantryItemId");

                    b.ToTable("PantryItems");
                });
#pragma warning restore 612, 618
        }
    }
}
