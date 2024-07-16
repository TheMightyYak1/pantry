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
    public PantryItemType PantryItemType { get; set; }
    public int Quantity { get; set; }
    public UnitType Type { get; set; }

    public PantryItem(string name, PantryItemType pantryItemType)
    {
        PantryItemId = Guid.NewGuid();
        Name = name;
        PantryItemType = pantryItemType;
    }

}