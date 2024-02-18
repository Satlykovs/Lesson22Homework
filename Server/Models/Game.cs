namespace Server;

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

public class Game
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    [Required]
    [Range(0, 200)]
    public double Price { get; set; }

    [Required]
    [StringLength(2000, MinimumLength = 3)]
    public string Description { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Developer { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Publisher { get; set; }

    public Game(string name, double price, string description, string developer, string publisher)
    {
        Name = name;
        Price = price;
        Description = description;
        Developer = developer;
        Publisher = publisher;
    }
}