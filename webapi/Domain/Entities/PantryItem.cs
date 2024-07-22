using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Enums;

namespace Domain.Entities;
public class PantryItem
{
    public Guid PantryItemId { get; set;}
    public string Name { get; set; }
    public string Description { get; set; }
    public PantryItemType PantryItemType { get; set; }
    public UnitType UnitType { get; set; }

    public PantryItem(string name, string description, PantryItemType pantryItemType, UnitType unitType)
    {
        PantryItemId = Guid.NewGuid();
        Name = name;
        Description = description;
        PantryItemType = pantryItemType;
        UnitType = unitType;
    }

}