using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
public class User
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Bio { get; set; }
    public string Email { get; set; }
    public DateTime DateCreated { get; set; } 

    public IList<Recipe> Recipes { get; set; }


    public User(string username, string email)
    {
        UserId = Guid.NewGuid();
        Username = username;
        Email = email;
        DateCreated = DateTime.UtcNow;
    }
}