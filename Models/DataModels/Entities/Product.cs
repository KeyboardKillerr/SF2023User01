﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Entities;

public class Product
{
    [Key]
    public string Id { get; init; } = AutoGeneratedIdUnsafe();
    public int Price { get; set; }
    public int MaximumDiscount { get; set; }
    public int CurrentDiscount { get; set; }
    public int StorageQuantity { get; set; }
    public string Type { get; set; } = null!;
    public string MeasurmentUnit { get; set; } = null!;
    public string Manufacturer { get; set; } = null!;
    public string Dealer { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? ImagePath { get; set; }
    public virtual ICollection<OrdersProducts> OrdersProducts { get; set; } = new Collection<OrdersProducts>();

    internal static string AutoGeneratedIdUnsafe()
    {
        Random randomizer = new();
        var prefix = (char)randomizer.Next(65, 91);
        var suffix = (char)randomizer.Next(65, 91);
        var body = ConvertTo3DiditString(randomizer.Next(0, 1000));
        var end = randomizer.Next(0, 10).ToString();
        return $"{prefix}{body}{suffix}{end}";
    }
    private static string ConvertTo3DiditString(int number)
    {
        string result = "";

        if (number < 0) number *= -1;
        if (number / 1000 != 0) number %= 1000;

        if (number / 100 != 0) return number.ToString();
        else result += 0.ToString();

        if (number / 10 != 0) return number.ToString();
        else result += 0.ToString();

        result += number.ToString();
        return result;
    }
}
