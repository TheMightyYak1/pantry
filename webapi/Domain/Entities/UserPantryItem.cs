using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities;
public class UserPantryItem
{
    public Guid UserPantryItemId { get; set; }
    public Guid UserId { get; set; }
    public Guid PantryItemId { get; set; }
    public int Quantity { get; set; }

    [JsonIgnore]
    public User User { get; set; }

    [JsonIgnore]
    public PantryItem PantryItem { get; set; }

    public UserPantryItem(Guid userId, Guid pantryItemId, int quantity)
    {
        UserPantryItemId = Guid.NewGuid();
        UserId = userId;
        PantryItemId = pantryItemId;
        Quantity = quantity;
    }
}